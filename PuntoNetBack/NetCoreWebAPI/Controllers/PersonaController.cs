using AutoMapper;
using BusinessLayer.IBL;
using CsvHelper;
using CsvHelper.Configuration;
using DataAccessLayer.DAL;
using DataAccessLayer.Dtos.Persona;
using DataAccessLayer.Helpers;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Helpers;
using Shared.Enum;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/personas")]
    [ApiController]
    [Authorize(Roles = "ADMIN,PORTERO")]

    public class PersonaController : ControllerBase
    {
        private readonly IBL_Persona _bl;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public PersonaController(IBL_Persona bl, IMapper mapper, IWebHostEnvironment env)
        {
            _bl = bl;
            _mapper = mapper;
            _env = env;
        }

        //GET api/personas
        [HttpGet]
        public ActionResult<IEnumerable<PersonaReadDto>> GetAllPersonas()
        {
            var personas = _bl.GetAllPersonas();
            if (personas != null)
            {
                return Ok(_mapper.Map<IEnumerable<PersonaReadDto>>(personas));
            }
            else
            {
                return NotFound();
            }

        }

        //GET api/personas/{id}
        [HttpGet("{id}", Name = "GetPersonaById")]
        public ActionResult<PersonaReadDto> GetPersonaById(int id)
        {
            var persona = _bl.GetPersonaById(id);
            if (persona != null)
            {
                return Ok(_mapper.Map<PersonaReadDto>(persona));
            }
            return NotFound();
        }

        //POST api/persona
        [HttpPost]
        public async Task<ActionResult<PersonaReadDto>> CreatePersona([FromForm]PersonaCreateDto personaInfo)
        {
            if (_bl.GetPersonaByDocumento(personaInfo.nro_doc) != null)
            {
                throw new AppException("Ya hay una persona registrada con ese numero de documento");
            }
            var httpRequest = Request.Form;
            if (!httpRequest.Files.Any())
            {
                throw new AppException("Debe subir una foto");
            }
            var postedFile = httpRequest.Files[0];
            string filename = postedFile.FileName;
            var randomString = RandomString.RandomizeString(10);
            filename = randomString + filename;
            personaInfo.PhotoFileName = filename;
            var physicalPath = _env.ContentRootPath + "/Files/Photos/" + filename;
            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
            var persona = _mapper.Map<Persona>(personaInfo);
            var tenant = HttpContext.GetMultiTenantContext<Institucion>();

            await _bl.CreatePersonaConFoto(persona, postedFile.OpenReadStream(), tenant.TenantInfo.Id);
            //_bl.SaveChanges();

            //await DAL_FaceApi.AgregarPersona(persona.nro_doc, postedFile.OpenReadStream(),tenant.TenantInfo.Name);

            var personaReadDto = _mapper.Map<PersonaReadDto>(persona);

            return CreatedAtRoute(nameof(GetPersonaById), new { Id = personaReadDto.Id }, personaReadDto);
        }

        //PUT api/personas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersona(int id, [FromForm]PersonaUpdateDto personaUpdateDto)
        {
            var httpRequest = Request.Form;
            var personaModelFromRepo = _bl.GetPersonaById(id);
            if (string.IsNullOrEmpty(personaUpdateDto.PhotoFileName))
            {
                personaUpdateDto.PhotoFileName = personaModelFromRepo.PhotoFileName;
            }
            var tenant = HttpContext.GetMultiTenantContext<Institucion>();
            if (personaModelFromRepo.nro_doc != personaUpdateDto.nro_doc && _bl.GetPersonaByDocumento(personaUpdateDto.nro_doc) != null)
            {
                throw new AppException("Ya hay una persona registrada con ese numero de documento");
            }
            if (httpRequest.Files.Any())
            {
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var randomString = RandomString.RandomizeString(10);
                filename = randomString + filename;
                personaUpdateDto.PhotoFileName = filename;
                var physicalPath = _env.ContentRootPath + "/Files/Photos/" + filename;
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                await _bl.UpdatePersonaConFoto(personaModelFromRepo.nro_doc, personaUpdateDto.nro_doc, postedFile.OpenReadStream(), tenant.TenantInfo.Id);
            }
            string documentoViejo = personaModelFromRepo.nro_doc;
            _mapper.Map(personaUpdateDto, personaModelFromRepo);
            await _bl.UpdatePersona(personaModelFromRepo, documentoViejo, tenant.TenantInfo.Id);
            _bl.SaveChanges();
            return NoContent();
        }


        //DELETE api/personas/{id}
        [HttpDelete("{id}")]
        public ActionResult DeletePersona(int id)
        {

            var personaModelFromRepo = _bl.GetPersonaById(id);
            if (personaModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeletePersona(personaModelFromRepo);
            _bl.SaveChanges();
            var tenant = HttpContext.GetMultiTenantContext<Institucion>();
            try
            {
                DAL_FaceApi.BorrarPersona(personaModelFromRepo.nro_doc, tenant.TenantInfo.Id).Wait();
            }
            catch(Exception e)
            {
                return NoContent();
            }
            
            return NoContent();
        }

       
        [HttpPost("altaMasiva/{ruta}")]
        public ActionResult<IEnumerable<PersonaCreateDto>> AltaMasiva(string ruta)
        {
            if(ruta == null)
            {
                throw new AppException("Falta el archivo o el nombre no es valido");
            }
            var physicalPath = _env.ContentRootPath + "/Files/csvFiles/" + ruta;
            using (var reader = new StreamReader(physicalPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //chequear headers
                csv.Read();
                csv.ReadHeader();
                List<string> headers = csv.HeaderRecord.ToList();
                List<string> headersRequiered = HeadersPersonaCSV.HeaderCSV();
                foreach (var head in headersRequiered)
                {
                    if (!headers.Contains(head))
                    {
                        throw new AppException("Falta el campo " + head + " en el archivo");
                    }
                }
                //fin chequear headers
                var records = csv.GetRecords<PersonaCreateDto>().ToList();

                _bl.AltaMasivaPersona(records);
                return Ok(records);
            }
            
        }

        [HttpGet("busqueda")]
        public ActionResult<IEnumerable<PersonaReadDto>> Busqueda([FromQuery]string filter)
        {
            var personas = _bl.GetAllPersonasBusqueda(filter);
            if (personas != null)
            {
                return Ok(_mapper.Map<IEnumerable<PersonaReadDto>>(personas));
            }
            else
            {
                return NotFound();
            }
        }
    }
}

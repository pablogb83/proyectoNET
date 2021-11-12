﻿using AutoMapper;
using BusinessLayer.IBL;
using CsvHelper;
using CsvHelper.Configuration;
using DataAccessLayer.DAL;
using DataAccessLayer.Dtos.Persona;
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
        public async Task<ActionResult<PersonaReadDto>> CreatePersona()
        {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            
            string filename = postedFile.FileName;
            var randomString = RandomString.RandomizeString(10);
            filename = randomString + filename;

            Persona personaModel = new Persona();
            personaModel.Nombres = httpRequest["nombres"];
            personaModel.Apellidos = httpRequest["apellidos"];
            personaModel.Telefono = httpRequest["telefono"];
            personaModel.Email = httpRequest["email"];
            personaModel.tipo_doc = (TipoDocumento)Int32.Parse(httpRequest["tipo_doc"]);
            personaModel.nro_doc = httpRequest["nro_doc"];
            personaModel.PhotoFileName = filename;

            var physicalPath = _env.ContentRootPath + "/Files/Photos/" + filename;
            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }
            _bl.CreatePersona(personaModel);
            _bl.SaveChanges();

            await DAL_FaceApi.AgregarPersona(personaModel.nro_doc, postedFile.OpenReadStream());

            var personaReadDto = _mapper.Map<PersonaReadDto>(personaModel);

            return CreatedAtRoute(nameof(GetPersonaById), new { Id = personaReadDto.Id }, personaReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/personas/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePersona(int id, PersonaUpdateDto personaUpdateDto)
        {
            var personaModelFromRepo = _bl.GetPersonaById(id);
            if (personaModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(personaUpdateDto, personaModelFromRepo);
            _bl.UpdatePersona(personaModelFromRepo);
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
            return NoContent();
        }

       
        [HttpPost("altaMasiva/{ruta}")]
        public ActionResult<IEnumerable<PersonaCreateDto>> AltaMasiva(string ruta)
        {
            //var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            //{
            //    //PrepareHeaderForMatch = args => args.Header.ToLower(),
            //    Encoding = Encoding.Unicode
            //};
            var physicalPath = _env.ContentRootPath + "/Files/csvFiles/" + ruta;
            //var streamReader = new StreamReader(physicalPath, Encoding.Unicode);
            ////var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Encoding = Encoding.Unicode };
            ////var physicalPath = _env.ContentRootPath + "/Photos/" + ruta;
            ////var reader = new StreamReader(physicalPath);
            //var csv = new CsvReader(streamReader, config);
            //var records = csv.GetRecords<PersonaCreateDto>().ToArray();
            //return Ok(records);

            using (var reader = new StreamReader(physicalPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
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
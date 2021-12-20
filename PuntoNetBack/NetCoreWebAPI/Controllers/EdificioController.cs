using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Edificios;
using DataAccessLayer.Dtos.PuertaAccesos;
using DataAccessLayer.Dtos.Salon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    //api/instituciones
    [Route("api/edificios")]
    [ApiController]
    public class EdificioController : ControllerBase
    {
        private readonly IBL_Edificio _bl;
        private readonly IBL_UsuarioEdificio _blusrEd;
        private readonly IMapper _mapper;
        private readonly ILogger<EdificioController> _logger;


        public EdificioController(IBL_Edificio bl, IMapper mapper, ILogger<EdificioController> logger, IBL_UsuarioEdificio blusrEd)
        {
            _bl = bl;
            _mapper = mapper;
            _logger = logger;
            _blusrEd = blusrEd;
        }

        //GET api/edificios
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<IEnumerable<EdificiosReadDto>> GetAllEdificios()
        {
            var edificios = _bl.GetAllEdificios();
            if (edificios != null)
            {
                return Ok(_mapper.Map<IEnumerable<EdificiosReadDto>>(edificios));
            }
            else
            {
                return NotFound();
            }
            
        }

        //GET api/edificios/{id}
        [HttpGet("{id}", Name = "GetEdificioById")]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public async Task<ActionResult<EdificiosReadDto>> GetEdificioById(int id)
        {
           
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role == "SUPERADMIN" || role=="ADMIN")
            {
                var edificio = _bl.GetEdificioById(id);
                if (edificio != null)
                {
                    return Ok(_mapper.Map<EdificiosReadDto>(edificio));
                }
                return NotFound();
            }
            else
            {
                int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
                var edificio = await _blusrEd.GetEdificioUsuario(idUsuario);
                return Ok(_mapper.Map<EdificiosReadDto>(edificio));
            }
        }

        //POST api/edificio
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<EdificiosReadDto> CreateEdificio(EdificioCreateDto edificioCreateDto)
        {

            var edificioModel = _mapper.Map<Edificio>(edificioCreateDto);
            _bl.CreateEdificio(edificioModel);
            _bl.SaveChanges();

            var edificioReadDto = _mapper.Map<EdificiosReadDto>(edificioModel);

            return CreatedAtRoute(nameof(GetEdificioById), new { Id = edificioReadDto.Id }, edificioReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdateEdificio(int id, EdificioUpdateDto edificioUpdateDto)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }

            string datosAntesDelcambio = "Nombre: " + edificioModelFromRepo.Nombre +
                                         " Direccion: " +  edificioModelFromRepo.Direccion +
                                         " Telefono:" +  edificioModelFromRepo.Telefono;
            _logger.LogInformation(message: "EdificioAntes: " + datosAntesDelcambio);
            _mapper.Map(edificioUpdateDto, edificioModelFromRepo);
            _bl.UpdateEdificio(edificioModelFromRepo);
            _bl.SaveChanges();
            string datosDespuesDelCambio = "Nombre: "  + edificioUpdateDto.Nombre + 
                                           " Dirección: " + edificioUpdateDto.Direccion + 
                                           " Teléfono: " + edificioUpdateDto.Telefono;
            _logger.LogInformation(message: "EdificioDespués: " + datosDespuesDelCambio);
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult PartialEdificioUpdtate(int id, JsonPatchDocument<EdificioUpdateDto> patchDoc)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }

            var edificioToPatch = _mapper.Map<EdificioUpdateDto>(edificioModelFromRepo);
            patchDoc.ApplyTo(edificioToPatch, ModelState);
            if (!TryValidateModel(edificioToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(edificioToPatch, edificioModelFromRepo);
            _bl.UpdateEdificio(edificioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult DeleteEdificio(int id)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteEdificio(edificioModelFromRepo);
            _bl.SaveChanges();
            string datosAntesDelcambio = "Nombre: " + edificioModelFromRepo.Nombre +
                             " Direccion: " + edificioModelFromRepo.Direccion +
                             " Telefono:" + edificioModelFromRepo.Telefono;
            _logger.LogInformation(message: "EdificioBorrado: " + datosAntesDelcambio);
            return Ok(new { message="Edificio eliminado" });
        }

        [HttpGet("salones/{id}")]
        [Authorize(Roles = "ADMIN, GESTOR")]
        public ActionResult <IEnumerable<SalonReadDto>> GetSalones(int id)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<SalonReadDto>>(edificioModelFromRepo.Salones));

        }

        [HttpGet("puertas/{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult<IEnumerable<SalonReadDto>> GetPuertas(int id)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<PuertaReadDto>>(edificioModelFromRepo.puerta_accesos));

        }
    }
}
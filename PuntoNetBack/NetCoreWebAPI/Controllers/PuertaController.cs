using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.PuertaAccesos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreWebAPI.Controllers
{
    //api/instituciones
    [Route("api/puerta")]
    [ApiController]
    public class PuertaController : ControllerBase
    {
        private readonly IBL_Puerta _bl;
        private readonly IBL_Acceso _blAcc;
        private readonly IMapper _mapper;
        private readonly ILogger<PuertaController> _logger;

        public PuertaController(IBL_Puerta bl, IMapper mapper, ILogger<PuertaController> logger, IBL_Acceso blAcc)
        {
            _bl = bl;
            _mapper = mapper;
            _logger = logger;
            _blAcc = blAcc;
        }

        //GET api/puertas
        [HttpGet]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult<IEnumerable<PuertaReadDto>> GetAllPuertas()
        {
            var puertas = _bl.GetAllPuertas();
            return Ok(_mapper.Map<IEnumerable<PuertaReadDto>>(puertas));
        }

        //GET api/puertas/{id}
        [HttpGet("{id}", Name = "GetPuertaById")]
        [Authorize(Roles = "ADMIN,PORTERO")]

        public ActionResult<PuertaReadDto> GetPuertaById(int id)
        {
            var puerta = _bl.GetPuertaById(id);
            if (puerta != null)
            {
                return Ok(_mapper.Map<PuertaReadDto>(puerta));
            }
            return NotFound();
        }

        //POST api/puertas
        [HttpPost]
        [Authorize(Roles = "ADMIN")]

        public ActionResult<PuertaReadDto> CreatePuerta(PuertaCreateDto puertaCreateDto)
        {
            var puertaModel = _mapper.Map<Puerta>(puertaCreateDto);
            _bl.CreatePuerta(puertaModel, puertaCreateDto.idEdificio);
            _bl.SaveChanges();

            var puertaReadDto = _mapper.Map<PuertaReadDto>(puertaModel);

            return CreatedAtRoute(nameof(GetPuertaById), new { Id = puertaReadDto.Id }, puertaReadDto);
        }

        //DELETE api/puerta/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]

        public ActionResult DeletePuerta(int id)
        {
            var edificioModelFromRepo = _bl.GetPuertaById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeletePuerta(edificioModelFromRepo);
            _bl.SaveChanges();
            string puertaEliminada = " Denominacion: " + edificioModelFromRepo.Denominacion;
            _logger.LogInformation(message: "PuertaEliminada: " + puertaEliminada);
            return Ok(new { message = "Eliminado correctamente" });
        }

        //PUT api/salon/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]

        public ActionResult UpdatePuerta(int id, PuertaUpdateDto puertaUpdateDto)
        {
            var puertaModelFromRepo = _bl.GetPuertaById(id);
            if (puertaModelFromRepo == null)
            {
                return NotFound();
            }
            string datosAntesdelCambio = "Edificio: " + puertaModelFromRepo.edificio.Nombre +
                                         " Denominacion: " + puertaModelFromRepo.Denominacion;

            _logger.LogInformation(message: "PuertaAntes: " + datosAntesdelCambio);
            _mapper.Map(puertaUpdateDto, puertaModelFromRepo);
            _bl.UpdatePuerta(puertaModelFromRepo.Id);
            _bl.SaveChanges();

            string datosDespuesdelCambio = " Denominacion: " + puertaUpdateDto.Denominacion;
            _logger.LogInformation(message: "PuertaDespues: " + datosDespuesdelCambio);
            return NoContent();
        }
    }
}
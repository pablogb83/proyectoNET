using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Salon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/salon")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]

    public class SalonController : ControllerBase
    {
        private readonly IBL_Salon _bl;
        private readonly IMapper _mapper;
        private readonly ILogger<SalonController> _logger;

        public SalonController(IBL_Salon bl, IMapper mapper, ILogger<SalonController> logger)
        {
            _bl = bl;
            _mapper = mapper;
            _logger = logger;
        }

        //GET api/salon
        [HttpGet]

        public ActionResult<IEnumerable<SalonReadDto>> GetAllSalones()
        {
            var salones = _bl.GetAllSalon(); ;
            return Ok(_mapper.Map<IEnumerable<SalonReadDto>>(salones));
        }

        //GET api/salon/{id}
        [HttpGet("{id}", Name = "GetSalonById")]

        public ActionResult<SalonReadDto> GetSalonById(int id)
        {
            var salon = _bl.GetSalonById(id);
            if (salon != null)
            {
                return Ok(_mapper.Map<SalonReadDto>(salon));
            }
            return NotFound();
        }

        //POST api/salon
        [HttpPost]
        public ActionResult<SalonReadDto> CreateSalon(SalonCreateDto salonCreateDto)
        {
            var salonModel = _mapper.Map<Salon>(salonCreateDto);
            _bl.CreateSalon(salonModel, salonCreateDto.idEdificio);
            _bl.SaveChanges();

            var salonReadDto = _mapper.Map<SalonReadDto>(salonModel);

            return CreatedAtRoute(nameof(GetSalonById), new { Id = salonReadDto.Id }, salonReadDto);
        }

        //DELETE api/salon/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteSalon(int id)
        {
            var salonModelFromRepo = _bl.GetSalonById(id);
            if (salonModelFromRepo == null)
            {
                return NotFound();
            }
            var eventos = salonModelFromRepo.Eventos;
            if (eventos.Any())
            {
                return BadRequest(new { message = "El Salon ya esta asignado a eventos" });
            }
            _bl.DeleteSalon(salonModelFromRepo);
            _bl.SaveChanges();
            string salonEliminado = " Denominacion: " + salonModelFromRepo.Denominacion +
                                    " Numero:" + salonModelFromRepo.Numero;
            _logger.LogInformation(message: "SalonEliminado: " + salonEliminado);
            return Ok(new { message="Eliminado correctamente" });
        }

        //PUT api/salon/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateSalon(int id, SalonUpdateDto salonUpdateDto)
        {
            var salonModelFromRepo = _bl.GetSalonById(id);
            if (salonModelFromRepo == null)
            {
                return NotFound();
            }
            string datosAntesDelcambio = "Edificio: " + salonModelFromRepo.edificio.Nombre +
                                         " Denominacion: " + salonModelFromRepo.Denominacion +
                                         " Numero: " + salonModelFromRepo.Numero;
            _logger.LogInformation(message: "SalonAntes: " + datosAntesDelcambio);

            _mapper.Map(salonUpdateDto, salonModelFromRepo);
            _bl.UpdateSalon(salonModelFromRepo);
            _bl.SaveChanges();
            string datosDespuesDelcambio =  " Denominacion: " + salonUpdateDto.Denominacion +
                                            " Numero: " + salonUpdateDto.numero;
            _logger.LogInformation(message: "SalonDespues: " + datosDespuesDelcambio);

            return NoContent();
        }
    }
}

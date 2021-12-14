using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Accesos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/accesos")]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IBL_Acceso _bl;
        private readonly IMapper _mapper;

        public AccesoController(IBL_Acceso bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<IEnumerable<AccesoReadDto>> GetAllAccesos()
        {
            var accesos = _bl.GetAllAccesos();
            if (accesos != null)
            {
                return Ok(_mapper.Map<IEnumerable<AccesoReadDto>>(accesos));
            }
            else
            {
                return NotFound();
            }

        }


        [HttpGet("{id}", Name = "GetAccesoById")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<AccesoReadDto> GetAccesoById(int id)
        {
            var acceso = _bl.GetAccesoById(id);
            if (acceso != null)
            {
                return Ok(_mapper.Map<AccesoReadDto>(acceso));
            }
            return NotFound("No se econtro el acceso");
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult<AccesoReadDto> CreateAcceso(AccesoCreateDto accesoCreateDto)
        {
            try
            {
                var accesoModel = _mapper.Map<Acceso>(accesoCreateDto);
                _bl.CreateAcceso(accesoModel, accesoCreateDto.PersonaId, accesoCreateDto.PuertaId);
                _bl.SaveChanges();

                var accesoReadDto = _mapper.Map<AccesoReadDto>(accesoModel);

                return CreatedAtRoute(nameof(GetAccesoById), new { Id = accesoReadDto.Id }, accesoReadDto);
                //return Ok(commandReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult UpdateAcceso(int id, AccesoUpdateDto accesoUpdateDto)
        {
            var accesoModelFromRepo = _bl.GetAccesoById(id);
            if (accesoModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(accesoUpdateDto, accesoModelFromRepo);
            _bl.UpdateAcceso(accesoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult DeleteAcceso(int id)
        {
            var accesoModelFromRepo = _bl.GetAccesoById(id);
            if (accesoModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteAcceso(accesoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        [HttpGet("edificio/{id}")]
        [Authorize(Roles = "ADMIN")]

        public ActionResult<IEnumerable<AccesoReadDto>> GetAccesosEdificio(int id)
        {
            var accesosEdificio = _bl.GetAccesosEdificio(id);
            if (accesosEdificio == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AccesoReadDto>>(accesosEdificio));

        }

        [HttpGet("puertas/{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]

        public ActionResult<IEnumerable<AccesoReadDto>> GetAccesosPuerta(int id)
        {
            var accesosPuerta = _bl.GetAccesosPuerta(id);
            if (accesosPuerta == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AccesoReadDto>>(accesosPuerta));

        }

        [HttpGet("persona/{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]
        public ActionResult<IEnumerable<AccesoReadDto>> GetAccesosPersona(int id)
        {
            var accesosPersona = _bl.GetAccesosPersona(id);
            if (accesosPersona == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AccesoReadDto>>(accesosPersona));

        }
    }
}

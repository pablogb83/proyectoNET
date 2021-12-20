using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Eventos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System.Collections.Generic;
using DataAccessLayer.Dtos.Salon;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    [Authorize(Roles = "ADMIN,GESTOR")]

    public class EventoController : ControllerBase
    {
        private readonly IBL_Evento _bl;
        private readonly IBL_UsuarioEdificio _blUsrEd;
        private readonly IMapper _mapper;


        public EventoController(IBL_Evento bl, IMapper mapper, IBL_UsuarioEdificio blUsrEd)
        {
            _bl = bl;
            _mapper = mapper;
            _blUsrEd = blUsrEd;
        }


        //GET api/eventos
        [HttpGet]

        public ActionResult<IEnumerable<EventosReadDto>> GetAllEventos()
        {
            var eventos = _bl.GetAllEventos();
            return Ok(_mapper.Map<IEnumerable<EventosReadDto>>(eventos));
        }

        [HttpGet("edificio")]
        public async Task<ActionResult<IEnumerable<EventosReadDto>>> GetAllEventosEdificio()
        {
            int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
            var edificioUsuario = await _blUsrEd.GetEdificioUsuario(idUsuario);
            if (edificioUsuario != null)
            {
                var eventos = _bl.GetAllEventosEdificio(edificioUsuario.Id);
                return Ok(_mapper.Map<IEnumerable<EventosReadDto>>(eventos));
            }
            else
            {
                return BadRequest(new { message = "El usuario no tiene edificio asignado" });
            }
        }

        //GET api/eventos/{id}
        [HttpGet("{id}", Name = "GetEventoById")]

        public ActionResult<EventosReadDto> GetEventoById(int id)
        {
            var evento = _bl.GetEventoById(id);
            if (evento != null)
            {
                return Ok(_mapper.Map<EventosReadDto>(evento));
            }
            return NotFound();
        }

        //POST api/evento
        [HttpPost]
        public async Task<ActionResult<EventosReadDto>> CreateEvento(EventoCreateDto eventoCreateDto)
        {
            
           
            if (eventoCreateDto.FechaInicioEvt > eventoCreateDto.FechaFinEvt || eventoCreateDto.FechaInicioEvt < DateTime.Now)
            {
                return BadRequest(new { message = "La fecha de inicio debe ser anterior a la fecha de fin y mayor a la fecha actual" });
            }
            var cantHoras = eventoCreateDto.FechaFinEvt.Subtract(eventoCreateDto.FechaInicioEvt).TotalHours;
            if ( cantHoras > 12)
            {
                return BadRequest(new { message = "El evento debe tener minimo una hora y maximo 12" });
            }
            var eventoModel = _mapper.Map<Evento>(eventoCreateDto);
            int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role=="GESTOR" && !await _bl.VerificarEventoGestor(eventoCreateDto.SalonId, idUsuario))
            {
                return BadRequest(new { message = "Solo puede crear eventos de su edificio" });
            }
            _bl.CreateEvento(eventoModel, eventoCreateDto.SalonId);

            var eventoReadDto = _mapper.Map<EventosReadDto>(eventoModel);

            return CreatedAtRoute(nameof(GetEventoById), new { Id = eventoReadDto.Id }, eventoReadDto);
            //return Ok(commandReadDto);
        }

        [HttpPost("salonesdisponibles")]
        public ActionResult<IEnumerable<EventosReadDto>> GetSalonesDisponibles(SalonesDisponiblesDto datos)
        {
            if (datos.FechaInicioEvt > datos.FechaFinEvt || datos.FechaInicioEvt < DateTime.Now)
            {
                return BadRequest(new { message = "La fecha de inicio debe ser anterior a la fecha de fin y mayor a la fecha actual" });
            }
            var salones = _bl.GetSalonesDisponibles(datos);
            return Ok(_mapper.Map<IEnumerable<SalonReadDto>>(salones));
        }

        [HttpPost("recurrente")]
        public async Task<ActionResult> CreateEventoRecurrente(EventoRecurrenteCreateDto eventoCreateDto)
        {
            int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role == "GESTOR" && !await _bl.VerificarEventoGestor(eventoCreateDto.SalonId, idUsuario))
            {
                return BadRequest(new { message = "Solo puede crear eventos de su edificio" });
            }
            if (eventoCreateDto.FechaInicioEvt > eventoCreateDto.FechaFinEvt || eventoCreateDto.FechaInicioEvt < DateTime.Now)
            {
                return BadRequest(new { message = "La fecha de inicio debe ser anterior a la fecha de fin y mayor a la fecha actual" });
            }
            //poner aca alguna validacion y tirar las ecepciones
            _bl.CreateEventoRecurrente(eventoCreateDto, eventoCreateDto.SalonId);
            return Ok(new { message="Evento recurrente creado correctamente" });
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEvento(int id, EventoUpdateDto eventoUpdateDto)
        {
            var eventoModelFromRepo = _bl.GetEventoById(id);
            if (eventoUpdateDto.FechaInicioEvt > eventoUpdateDto.FechaFinEvt || eventoUpdateDto.FechaInicioEvt < DateTime.Now)
            {
                return BadRequest(new {message= "La fecha de inicio debe ser anterior a la fecha de fin y mayor a la fecha actual" });
            }
            var cantHoras = eventoUpdateDto.FechaFinEvt.Subtract(eventoUpdateDto.FechaInicioEvt);
            if (cantHoras.TotalHours > 12)
            {
                return BadRequest(new { message = "El evento debe tener minimo una hora y maximo 12" });
            }
            if (eventoModelFromRepo == null)
            {
                return NotFound();
            }
            int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role == "GESTOR" && !await _bl.VerificarEventoGestor(eventoModelFromRepo.Salon.Id, idUsuario))
            {
                return BadRequest(new { message = "Solo puede editar eventos de su edificio" });
            }
            _mapper.Map(eventoUpdateDto, eventoModelFromRepo);
            _bl.UpdateEvento(eventoModelFromRepo,eventoUpdateDto.SalonId);
            _bl.SaveChanges();
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialEventoUpdtate(int id, JsonPatchDocument<EventoUpdateDto> patchDoc)
        {
            var eventoModelFromRepo = _bl.GetEventoById(id);
            if (eventoModelFromRepo == null)
            {
                return NotFound();
            }

            var eventoToPatch = _mapper.Map<EventoUpdateDto>(eventoModelFromRepo);
            patchDoc.ApplyTo(eventoToPatch, ModelState);
            if (!TryValidateModel(eventoToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(eventoToPatch, eventoModelFromRepo);
            //_bl.UpdateEvento(eventoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvento(int id)
        {
            var eventoModelFromRepo = _bl.GetEventoById(id);
            int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role == "GESTOR" && !await _bl.VerificarEventoGestor(eventoModelFromRepo.Salon.Id, idUsuario))
            {
                return BadRequest(new { message = "Solo puede eliminar eventos de su edificio" });
            }
            if (eventoModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteEvento(eventoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

    }
}

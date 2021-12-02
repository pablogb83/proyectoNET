using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Eventos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataAccessLayer.Dtos.Salon;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IBL_Evento _bl;
        private readonly IMapper _mapper;


        public EventoController(IBL_Evento bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;

        }


        //GET api/eventos
        [HttpGet]
        public ActionResult<IEnumerable<EventosReadDto>> GetAllEventos()
        {
            var eventos = _bl.GetAllEventos();
            return Ok(_mapper.Map<IEnumerable<EventosReadDto>>(eventos));
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
        public ActionResult<EventosReadDto> CreateEvento(EventoCreateDto eventoCreateDto)
        {
            var eventoModel = _mapper.Map<Evento>(eventoCreateDto);
            _bl.CreateEvento(eventoModel, eventoCreateDto.SalonId);

            var eventoReadDto = _mapper.Map<EventosReadDto>(eventoModel);

            return CreatedAtRoute(nameof(GetEventoById), new { Id = eventoReadDto.Id }, eventoReadDto);
            //return Ok(commandReadDto);
        }

        [HttpGet("salonesdisponibles")]
        public ActionResult<IEnumerable<EventosReadDto>> GetSalonesDisponibles(DateTime fechainicio, DateTime fechafin)
        {
            var salones = _bl.GetSalonesDisponibles(fechainicio,fechafin);
            return Ok(_mapper.Map<IEnumerable<SalonReadDto>>(salones));
        }


        [HttpPost("recurrente")]
        public ActionResult CreateEventoRecurrente(EventoRecurrenteCreateDto eventoCreateDto)
        {
            //poner aca alguna validacion y tirar las ecepciones
            _bl.CreateEventoRecurrente(eventoCreateDto);
            return Ok(new { message="Evento recurrente creado correctamente" });
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEvento(int id, EventoUpdateDto eventoUpdateDto)
        {
            var eventoModelFromRepo = _bl.GetEventoById(id);
            if (eventoModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(eventoUpdateDto, eventoModelFromRepo);
            _bl.UpdateEvento(eventoModelFromRepo);
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
            _bl.UpdateEvento(eventoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteEvento(int id)
        {
            var eventoModelFromRepo = _bl.GetEventoById(id);
            if (eventoModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteEvento(eventoModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

   /*     [HttpPost]
        public string SaveFile(FileUpload fileObj) {
            Evento oEvent = JsonConvert.DeserializeObject<Evento>(fileObj.Evento);

            if (fileObj.file.Length > 0) 
            {
                using (var ms = new MemoryStream())
                {
                    fileObj.file.CopyTo(ms);

                    var fileBytes = ms.ToArray();

                    oEvent.Photo = fileBytes;

                    oEvent = _bl.CreateEvento(oEvent)
                }
            }
        }*/

    }
}

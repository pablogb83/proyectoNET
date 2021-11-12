using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.PuertaAccesos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;

namespace NetCoreWebAPI.Controllers
{
    //api/instituciones
    [Route("api/puerta")]
    [ApiController]
    public class PuertaController : ControllerBase
    {
        private readonly IBL_Puerta _bl;
        private readonly IMapper _mapper;

        public PuertaController(IBL_Puerta bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/puertas
        [HttpGet]
        public ActionResult<IEnumerable<PuertaReadDto>> GetAllPuertas()
        {
            var puertas = _bl.GetAllPuertas();
            return Ok(_mapper.Map<IEnumerable<PuertaReadDto>>(puertas));
        }

        //GET api/puertas/{id}
        [HttpGet("{id}", Name = "GetPuertaById")]
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
        public ActionResult DeletePuerta(int id)
        {
            var edificioModelFromRepo = _bl.GetPuertaById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeletePuerta(edificioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //PUT api/salon/{id}
        [HttpPut("{id}")]
        public ActionResult UpdatePuerta(int id, PuertaUpdateDto puertaUpdateDto)
        {
            var puertaModelFromRepo = _bl.GetPuertaById(id);
            if (puertaModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(puertaUpdateDto, puertaModelFromRepo);
            _bl.UpdatePuerta(puertaModelFromRepo.Id);
            _bl.SaveChanges();
            return NoContent();
        }
    }
}
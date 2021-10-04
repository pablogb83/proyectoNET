using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Edificios;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public EdificioController(IBL_Edificio bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/edificios
        [HttpGet]
        public ActionResult<IEnumerable<EdificiosReadDto>> GetAllEdificios()
        {
            var edificios = _bl.GetAllEdificios();
            return Ok(_mapper.Map<IEnumerable<EdificiosReadDto>>(edificios));
        }

        //GET api/edificios/{id}
        [HttpGet("{id}", Name = "GetEdificioById")]
        public ActionResult<EdificiosReadDto> GetEdificioById(int id)
        {
            var edificio = _bl.GetEdificioById(id);
            if (edificio != null)
            {
                return Ok(_mapper.Map<EdificiosReadDto>(edificio));
            }
            return NotFound();
        }

        //POST api/edificio
        [HttpPost]
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
        public ActionResult UpdateEdificio(int id, EdificioUpdateDto edificioUpdateDto)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(edificioUpdateDto, edificioModelFromRepo);
            _bl.UpdateEdificio(edificioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
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
        public ActionResult DeleteEdificio(int id)
        {
            var edificioModelFromRepo = _bl.GetEdificioById(id);
            if (edificioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteEdificio(edificioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }
    }
}

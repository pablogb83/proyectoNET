using System.Collections.Generic;
using AutoMapper;
using ProyectoNET.Dtos.Instituciones;
using ProyectoNET.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProyectoNET.Data;

namespace ProyectoNET.Controllers
{
    //api/instituciones
    [Route("api/instituciones")]
    [ApiController]
    public class InstitucionesController : ControllerBase
    {
        private readonly IInstitucionRepo _repository;
        private readonly IMapper _mapper;

        public InstitucionesController(IInstitucionRepo repository, IMapper mapper)
        {
          _repository = repository;
          _mapper = mapper;     
        }

        //GET api/instituciones
        [HttpGet]
        public ActionResult <IEnumerable<InstitucionesReadDto>> GetAllInstituciones()
        {
            var instituciones = _repository.GetAllInstituciones();
            return Ok(_mapper.Map<IEnumerable<InstitucionesReadDto>>(instituciones));
        }

        //GET api/instituciones/{id}
        [HttpGet("{id}", Name ="GetInstitucionById")]
        public ActionResult <InstitucionesReadDto> GetInstitucionById(int id)
        {
            var institucion = _repository.GetInstitucionById(id);
            if(institucion!=null)
            {
                return Ok(_mapper.Map<InstitucionesReadDto>(institucion));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult <InstitucionesReadDto> CreateInstitucion(InstitucionCreateDto instituionCreateDto)
        {
            var institucionModel = _mapper.Map<Institucion>(instituionCreateDto);
            _repository.CreateInstitucion(institucionModel);
            _repository.SaveChanges();

            var institucionReadDto = _mapper.Map<InstitucionesReadDto>(institucionModel);

            return CreatedAtRoute(nameof(GetInstitucionById), new {Id = institucionReadDto.Id}, institucionReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateInstitucion(int id, InstitucionUpdateDto institucionUpdateDto)
        {    
            var institucionModelFromRepo = _repository.GetInstitucionById(id);
            if(institucionModelFromRepo == null){
                return NotFound();
            }
            _mapper.Map(institucionUpdateDto, institucionModelFromRepo);
            _repository.UpdateInstitucion(institucionModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialInstitucionUpdtate(int id, JsonPatchDocument<InstitucionUpdateDto> patchDoc)
        {
            var institucionModelFromRepo = _repository.GetInstitucionById(id);
            if(institucionModelFromRepo == null){
                return NotFound();
            }

            var institucionToPatch = _mapper.Map<InstitucionUpdateDto>(institucionModelFromRepo);
            patchDoc.ApplyTo(institucionToPatch, ModelState);
            if(!TryValidateModel(institucionToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(institucionToPatch, institucionModelFromRepo);
            _repository.UpdateInstitucion(institucionModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteInstitucion(int id)
        {
            var institucionModelFromRepo = _repository.GetInstitucionById(id);
            if(institucionModelFromRepo == null){
                return NotFound();
            }
            _repository.DeleteInstitucion(institucionModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
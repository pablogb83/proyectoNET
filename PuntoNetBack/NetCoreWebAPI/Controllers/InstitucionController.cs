using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Instituciones;
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
    [Route("api/instituciones")]
    [ApiController]
    public class InstitucionController : ControllerBase
    {
        private readonly IBL_Institucion _bl;
        private readonly IMapper _mapper;

        public InstitucionController(IBL_Institucion bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/instituciones
        [HttpGet]
        public ActionResult<IEnumerable<InstitucionesReadDto>> GetAllInstituciones()
        {
            var instituciones = _bl.GetAllInstituciones();
            return Ok(_mapper.Map<IEnumerable<InstitucionesReadDto>>(instituciones));
        }

        //GET api/instituciones/{id}
        [HttpGet("{id}", Name = "GetInstitucionById")]
        public ActionResult<InstitucionesReadDto> GetInstitucionById(int id)
        {
            var institucion = _bl.GetInstitucionById(id);
            if (institucion != null)
            {
                return Ok(_mapper.Map<InstitucionesReadDto>(institucion));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<InstitucionesReadDto> CreateInstitucion(InstitucionCreateDto instituionCreateDto)
        {
            var institucionModel = _mapper.Map<Institucion>(instituionCreateDto);
            _bl.CreateInstitucion(institucionModel);
            _bl.SaveChanges();

            var institucionReadDto = _mapper.Map<InstitucionesReadDto>(institucionModel);

            return CreatedAtRoute(nameof(GetInstitucionById), new { Id = institucionReadDto.Id }, institucionReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateInstitucion(int id, InstitucionUpdateDto institucionUpdateDto)
        {
            var institucionModelFromRepo = _bl.GetInstitucionById(id);
            if (institucionModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(institucionUpdateDto, institucionModelFromRepo);
            _bl.UpdateInstitucion(institucionModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialInstitucionUpdtate(int id, JsonPatchDocument<InstitucionUpdateDto> patchDoc)
        {
            var institucionModelFromRepo = _bl.GetInstitucionById(id);
            if (institucionModelFromRepo == null)
            {
                return NotFound();
            }

            var institucionToPatch = _mapper.Map<InstitucionUpdateDto>(institucionModelFromRepo);
            patchDoc.ApplyTo(institucionToPatch, ModelState);
            if (!TryValidateModel(institucionToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(institucionToPatch, institucionModelFromRepo);
            _bl.UpdateInstitucion(institucionModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteInstitucion(int id)
        {
            var institucionModelFromRepo = _bl.GetInstitucionById(id);
            if (institucionModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteInstitucion(institucionModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }
    }
}


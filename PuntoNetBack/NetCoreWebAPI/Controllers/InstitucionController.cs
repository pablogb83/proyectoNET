﻿using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Instituciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ActionResult<InstitucionesReadDto> GetInstitucionById(string id)
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
            var institucionReadDto = _mapper.Map<InstitucionesReadDto>(institucionModel);
            institucionModel.Identifier = institucionModel.Id;
            _bl.UpdateInstitucion(institucionModel);
            _bl.SaveChanges();
            return CreatedAtRoute(nameof(GetInstitucionById), new { Id = institucionReadDto.Id }, institucionReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateInstitucion(string id, InstitucionUpdateDto institucionUpdateDto)
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
        public ActionResult PartialInstitucionUpdtate(string id, JsonPatchDocument<InstitucionUpdateDto> patchDoc)
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

        [HttpGet("active", Name = "IsActive")]
        [Authorize(Roles = "ADMIN, PORTERO, GESTOR")]  
        public ActionResult<bool> IsActive()
        {
            var tenant = User.Claims.Skip(1).FirstOrDefault();
            string id = tenant.Value;
            if (string.IsNullOrEmpty(id))
            {
                id = "";
            }
            var institucion = _bl.GetInstitucionById(id);
            return Ok(institucion != null && institucion.Activa);
        }

        [HttpGet("facturacion")]
        //[Authorize(Roles = "ADMIN, PORTERO, GESTOR")]
        public ActionResult<bool> GetFacturacion(string id, DateTime fechainicio, DateTime fechafin)
        {
            var role = User.Claims.Skip(2).FirstOrDefault().Value;
            if (role == "SUPERADMIN")
            {
                return Ok(_bl.GetFacturacion(id, fechainicio, fechafin));
            }
            else
            {
                var tenant = User.Claims.Skip(1).FirstOrDefault().Value;
                return Ok(_bl.GetFacturacion(tenant, fechainicio, fechafin));
            }
        }

        [HttpGet("admin")]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<bool> InstitucionAdmin()
        {
            var tenant = User.Claims.Skip(1).FirstOrDefault();
            string id = tenant.Value;
            if (string.IsNullOrEmpty(id))
            {
                id = "";
            }
            var institucion = _bl.GetInstitucionById(id);
            if (institucion!=null)
            {
                return Ok(institucion);
            }
            else
            {
                return BadRequest();
            }
        }


        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteInstitucion(string id)
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

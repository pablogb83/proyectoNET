using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Roles;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    //api/roles
    [Route("api/roles")]
    
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IBL_Role _bl;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(IBL_Role bl, IMapper mapper, RoleManager<Role> roleManager)
        {
            _bl = bl;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        //GET api/roles
        [HttpGet]
        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        public ActionResult<IEnumerable<RolesReadDto>> GetAllRoles()
        {
            var roles = _bl.GetAllRoles();
            return Ok(_mapper.Map<IEnumerable<RolesReadDto>>(roles));
        }

        //GET api/roles/{id}
        [HttpGet("{id}", Name = "GetRoleById")]
        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        public ActionResult<RolesReadDto> GetRoleById(int id)
        {
            var role = _bl.GetRoleById(id);
            if (role != null)
            {
                return Ok(_mapper.Map<RolesReadDto>(role));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        [Authorize(Roles = "SUPERADMIN")]
        public async Task<ActionResult<RolesReadDto>> CreateRolAsync(RoleCreateDto roleCreateDto)
        {
            var roleModel = _mapper.Map<Role>(roleCreateDto);
            await _bl.CreateRoleAsync(roleModel);
            //_bl.SaveChanges();
            //var result = await _roleManager.CreateAsync(roleModel);

            var roleReadDto = _mapper.Map<RolesReadDto>(roleModel);

            return CreatedAtRoute(nameof(GetRoleById), new { Id = roleReadDto.Id }, roleReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "SUPERADMIN")]

        public ActionResult UpdateRole(int id, RoleUpdateDto institucionUpdateDto)
        {
            var roleModelFromRepo = _bl.GetRoleById(id);
            if (roleModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(institucionUpdateDto, roleModelFromRepo);
            _bl.UpdateRole(roleModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "SUPERADMIN")]

        public ActionResult PartialRoleUpdtate(int id, JsonPatchDocument<RoleUpdateDto> patchDoc)
        {
            var roleModelFromRepo = _bl.GetRoleById(id);
            if (roleModelFromRepo == null)
            {
                return NotFound();
            }

            var institucionToPatch = _mapper.Map<RoleUpdateDto>(roleModelFromRepo);
            patchDoc.ApplyTo(institucionToPatch, ModelState);
            if (!TryValidateModel(institucionToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(institucionToPatch, roleModelFromRepo);
            _bl.UpdateRole(roleModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "SUPERADMIN")]

        public ActionResult DeleteRole(int id)
        {
            var roleModelFromRepo = _bl.GetRoleById(id);
            if (roleModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteRole(roleModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

       
    }
}
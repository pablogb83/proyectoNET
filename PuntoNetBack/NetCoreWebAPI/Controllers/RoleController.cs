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

    }   
}
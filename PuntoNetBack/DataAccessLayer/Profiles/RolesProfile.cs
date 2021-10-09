using AutoMapper;
using DataAccessLayer.Dtos.Roles;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            //Source -> Target
            CreateMap<Role, RolesReadDto>();
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
            CreateMap<Role, RoleUpdateDto>();
        }
    }
}
using AutoMapper;
using DataAccessLayer.Dtos.Usuarios;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            //Source -> Target
            CreateMap<Usuario, UsuarioReadDto>();
            CreateMap<UsuarioCreateDto, Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>();
            CreateMap<Usuario, UsuarioUpdateDto>();
        }
    }
}

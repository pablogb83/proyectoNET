using AutoMapper;
using DataAccessLayer.Dtos.UsuarioEdificio;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class UsuarioEdificioProfile : Profile
    {
        public UsuarioEdificioProfile()
        {
            CreateMap<UsuarioEdificio, UsuarioEdificioReadDto>();
            CreateMap<UsuarioEdificioCreateDto, UsuarioEdificio>();
        }
       
    }
}

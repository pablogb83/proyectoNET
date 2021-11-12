using AutoMapper;
using DataAccessLayer.Dtos.UsuarioPuerta;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class UsuarioPuertaProfile : Profile
    {
        public UsuarioPuertaProfile()
        {
            CreateMap<UsuarioPuerta, UsuarioPuertaReadDto>();
            CreateMap<UsuarioPuertaCreateDto, UsuarioPuerta>();
        }
    }
}

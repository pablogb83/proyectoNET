using AutoMapper;
using DataAccessLayer.Dtos.Accesos;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class AccesoProfile : Profile
    {
        public AccesoProfile()
        {
            //Source -> Target
            CreateMap<Acceso, AccesoReadDto>();
            CreateMap<AccesoCreateDto, Acceso>();
            CreateMap<AccesoUpdateDto, Acceso>();
            CreateMap<Acceso, AccesoUpdateDto>();
        }
    }
}

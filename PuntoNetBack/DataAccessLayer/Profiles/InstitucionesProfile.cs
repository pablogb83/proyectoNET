using AutoMapper;
using DataAccessLayer.Dtos.Instituciones;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class InstitucionesProfile : Profile
    {
        public InstitucionesProfile()
        {
            //Source -> Target
            CreateMap<Institucion, InstitucionesReadDto>();
            CreateMap<InstitucionCreateDto, Institucion>();
            CreateMap<InstitucionUpdateDto, Institucion>();
            CreateMap<Institucion, InstitucionUpdateDto>();
        }
    }
}

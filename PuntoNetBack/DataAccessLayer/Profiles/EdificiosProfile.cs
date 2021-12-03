using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccessLayer.Dtos.Edificios;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class EdificiosProfile : Profile
    {
        public EdificiosProfile()
        {
            //Source -> Target
            CreateMap<Edificio, EdificiosReadDto>();
            CreateMap<EdificioCreateDto, Edificio>();
            CreateMap<EdificioUpdateDto, Edificio>();
            CreateMap<Edificio, EdificioUpdateDto>();
        }
    }
}

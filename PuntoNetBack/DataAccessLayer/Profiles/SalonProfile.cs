using AutoMapper;
using DataAccessLayer.Dtos.Salon;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class SalonProfile : Profile
    {
        public SalonProfile()
        {
            //Source -> Target
            CreateMap<Salon, SalonReadDto>();
            CreateMap<SalonCreateDto, Salon>();
            CreateMap<SalonUpdateDto, Salon>();
            CreateMap<Salon, SalonUpdateDto>();
        }
    }
}

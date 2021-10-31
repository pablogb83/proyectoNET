using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccessLayer.Dtos.Edificios;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Dtos.PuertaAccesos;

namespace DataAccessLayer.Profiles
{
    public class PuertasProfile : Profile
    {
        public PuertasProfile()
        {
            //Source -> Target
            CreateMap<Puerta, PuertaReadDto>();
            CreateMap<PuertaCreateDto, Puerta>();
            CreateMap<PuertaUpdateDto, Puerta>();
            CreateMap<Puerta, PuertaUpdateDto>();
        }
    }
}

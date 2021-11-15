using AutoMapper;
using DataAccessLayer.Dtos.Persona;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class PersonaProfile : Profile
    {
        public PersonaProfile()
        {
            //Source -> Target
            CreateMap<Persona, PersonaReadDto>();
            CreateMap<PersonaCreateDto, Persona>();
            CreateMap<PersonaUpdateDto, Persona>();
            CreateMap<Persona, PersonaUpdateDto>();
        }
    }
}

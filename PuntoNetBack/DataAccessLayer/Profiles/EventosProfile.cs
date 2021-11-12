using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccessLayer.Dtos.Eventos;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Profiles
{
    public class EventosProfile : Profile
    {
        public EventosProfile()
        { 
            //Source -> Target
            CreateMap<Evento, EventosReadDto>();
            CreateMap<EventoCreateDto, Evento>();
            CreateMap<EventoUpdateDto, Evento>();
            CreateMap<Evento, EventoUpdateDto>();
        }
    }
}

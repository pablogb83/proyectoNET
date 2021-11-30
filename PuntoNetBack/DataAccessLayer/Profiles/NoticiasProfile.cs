using System;
using System.Collections.Generic;
using AutoMapper;
using DataAccessLayer.Dtos.Noticias;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Profiles
{
    public class NoticiasProfile : Profile
    {
        public NoticiasProfile()
        {
            //Source -> Target
            CreateMap<Noticias, NoticiaReadDto>();
            CreateMap<NoticiaCreateDto, Noticias>();
            CreateMap<NoticiaUpdateDto, Noticias>();
            CreateMap<Noticias, NoticiaUpdateDto>();
        }
    }
}

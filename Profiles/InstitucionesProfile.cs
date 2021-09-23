using AutoMapper;
using ProyectoNET.Dtos.Instituciones;
using ProyectoNET.Models;

namespace ProyectoNET.Profiles
{
    public class InstitucionesProfile : Profile
    {
        public InstitucionesProfile()
        {
            //Source -> Target
            CreateMap<Institucion, InstitucionesReadDto>();
            CreateMap<InstitucionCreateDto,Institucion>();
            CreateMap<InstitucionUpdateDto, Institucion>();
            CreateMap<Institucion, InstitucionUpdateDto>();
        }
    }
}
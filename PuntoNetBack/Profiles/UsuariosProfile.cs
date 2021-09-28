using AutoMapper;
using ProyectoNET.Dtos.Usuarios;
using ProyectoNET.Models;

namespace ProyectoNET.Profiles
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            //Source -> Target
            CreateMap<Usuario, UsuarioReadDto>();
            CreateMap<UsuarioCreateDto,Usuario>();
            CreateMap<UsuarioUpdateDto, Usuario>();
            CreateMap<Usuario, UsuarioUpdateDto>();
        }
    }
}
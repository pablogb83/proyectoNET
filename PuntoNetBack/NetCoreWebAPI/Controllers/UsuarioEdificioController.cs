using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.UsuarioEdificio;
using DataAccessLayer.Dtos.Usuarios;

using Microsoft.AspNetCore.Mvc;
using NetCoreWebAPI.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/usuarioEdificio")]
    [ApiController]
    public class UsuarioEdificioController : ControllerBase
    {
        private readonly IBL_UsuarioEdificio _bl;
        private readonly IMapper _mapper;

        public UsuarioEdificioController(IBL_UsuarioEdificio bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/usuarioEdificio
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioEdificioReadDto>> GetAllUsuariosEdificios()
        {
            var usuarioEdificios = _bl.GetAllUsuarioEdificio(); ;
            return Ok(_mapper.Map<IEnumerable<UsuarioEdificioReadDto>>(usuarioEdificios));
        }

        [Authorize(Role = "ADMIN")]
        //POST api/usuarioEdificio
        [HttpPost]
        public ActionResult<UsuarioEdificioReadDto> CreateUsuarioEdificio(UsuarioEdificioCreateDto usuarioEdificioCreateDto)
        {
            //var usuarioEdificioModel = _mapper.Map<UsuarioEdificio>(UsuarioEdificioCreateDto);
            try
            {
                if (_bl.CreateUsuarioEdificio(usuarioEdificioCreateDto.UsuarioId, usuarioEdificioCreateDto.EdificioId)) 
                {
                    _bl.SaveChanges();
                    return Ok(new { msg = "Usuario agregado correctamente" });
                }
                else
                {
                    throw new ArgumentException(
                      "No se puede asignar ese usuario a ese edificio ");
                }
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/usuarioEdificio/id
        [HttpGet ("{id}", Name = "GetUsuariosEdificios")]
        public ActionResult<IEnumerable<UsuarioReadDto>> GetUsuariosEdificios(int id)
        {
            var usuarios = _bl.GetUsuariosEdificio(id); 
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(usuarios));
        }

        //DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUsuarioEdificio(int id)
        {
            try
            {
                _bl.DeleteUsuarioEdificio(id);
                _bl.SaveChanges();
                return Ok("Eliminado correctamente");
            }
            catch
            {
                return BadRequest("Algo salio mal");
            }
  
            
        }
    }
}

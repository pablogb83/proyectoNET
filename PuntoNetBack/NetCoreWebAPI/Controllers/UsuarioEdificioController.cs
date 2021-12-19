using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Edificios;
using DataAccessLayer.Dtos.UsuarioEdificio;
using DataAccessLayer.Dtos.Usuarios;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "ADMIN")]

        public async Task<ActionResult<IEnumerable<UsuarioEdificioReadDto>>> GetAllUsuariosEdificios()
        {
            var usuarioEdificios =await _bl.GetAllUsuarioEdificio(); ;
            return Ok(_mapper.Map<IEnumerable<UsuarioEdificioReadDto>>(usuarioEdificios));
        }

        //POST api/usuarioEdificio
        [HttpPost]
        [Authorize(Roles = "ADMIN")]

        public async Task<ActionResult<UsuarioEdificioReadDto>> CreateUsuarioEdificioAsync(UsuarioEdificioCreateDto usuarioEdificioCreateDto)
        {
            //var usuarioEdificioModel = _mapper.Map<UsuarioEdificio>(UsuarioEdificioCreateDto);
            try
            {
                if (await _bl.CreateUsuarioEdificioAsync(usuarioEdificioCreateDto.UsuarioId, usuarioEdificioCreateDto.EdificioId))
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/usuarioEdificio/id
        [HttpGet("{id}", Name = "GetUsuariosEdificios")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetUsuariosEdificios(int id)
        {
            var usuarios = await _bl.GetUsuariosEdificio(id);
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(usuarios));
        }

        //GET api/usuarioEdificio/id
        [HttpGet("edificio/{id}")]
        [Authorize(Roles = "ADMIN,PORTERO,GESTOR")]

        public async Task<ActionResult<IEnumerable<EdificiosReadDto>>> GetEdificioUsuario(int id)
        {
            try
            {
                
                var role = User.Claims.Skip(2).FirstOrDefault().Value;
                if (role == "SUPERADMIN")
                {
                    var edificio = await _bl.GetEdificioUsuario(id);
                    return Ok(_mapper.Map<EdificiosReadDto>(edificio));
                }
                else
                {
                    int idUsuario = int.Parse(User.Claims.FirstOrDefault().Value);
                    var edificio = await _bl.GetEdificioUsuario(idUsuario);
                    return Ok(_mapper.Map<EdificiosReadDto>(edificio));
                }
            }
            catch
            {
                return BadRequest("Algo salio mal");
            }

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
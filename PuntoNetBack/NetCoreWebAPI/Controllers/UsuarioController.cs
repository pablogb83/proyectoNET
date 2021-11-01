using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Usuarios;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreWebAPI.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    //api/Usuario
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IBL_Usuario _bl;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsuarioController(IBL_Usuario bl, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _bl = bl;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UsuarioAutenticateDto model)
        {
            var user = _bl.Autenticar(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario o password incorrectos" });

            if(user.Role == null)
                return BadRequest(new { message = "Usuario sin rol asignado, contacte a su administrador" });
     
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("TenantId", user.TenantId)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = tokenString,
                TenantId = user.TenantId,
                Role = user.Role.NombreRol
            }) ;
        }

        //GET api/usuarios

        [Authorize(Role = "ADMIN")]
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioReadDto>> GetAllUsuarios()
        {
            //var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var Usuario = _bl.GetAllUsuarios();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(Usuario));
        }

        //GET api/usuarios/{id}
        [HttpGet("{id}", Name = "GetUsuarioById")]
        public ActionResult<UsuarioReadDto> GetUsuarioById(int id)
        {
            var Usuario = _bl.GetUsuarioById(id);
            if (Usuario != null)
            {
                return Ok(_mapper.Map<UsuarioReadDto>(Usuario));
            }
            return NotFound();
        }

        //POST api/usuarios
        [HttpPost]
        public ActionResult<UsuarioReadDto> CreateUsuario([FromBody] UsuarioCreateDto usuarioCreateDto)
        {
            var UsuarioModel = _mapper.Map<Usuario>(usuarioCreateDto);
            try
            {
                _bl.CreateUsuario(UsuarioModel, usuarioCreateDto.PasswordPlano);
                _bl.SaveChanges();

                var UsuarioReadDto = _mapper.Map<UsuarioReadDto>(UsuarioModel);

                return CreatedAtRoute(nameof(GetUsuarioById), new { Id = UsuarioReadDto.Id }, UsuarioReadDto);
                //return Ok(commandReadDto);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        //PUT api/usuarios/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUsuario(int id, UsuarioUpdateDto UsuarioUpdateDto)
        {
            var UsuarioModelFromRepo = _bl.GetUsuarioById(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(UsuarioUpdateDto, UsuarioModelFromRepo);
            _bl.UpdateUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //PATCH api/usuarios/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUsuarioUpdtate(int id, JsonPatchDocument<UsuarioUpdateDto> patchDoc)
        {
            var UsuarioModelFromRepo = _bl.GetUsuarioById(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }

            var UsuarioToPatch = _mapper.Map<UsuarioUpdateDto>(UsuarioModelFromRepo);
            patchDoc.ApplyTo(UsuarioToPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);
            if (!TryValidateModel(UsuarioToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(UsuarioToPatch, UsuarioModelFromRepo);
            _bl.UpdateUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

        //DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var UsuarioModelFromRepo = _bl.GetUsuarioById(id);
            if (UsuarioModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteUsuario(UsuarioModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }
    }
}


using System.Collections.Generic;
using AutoMapper;
using ProyectoNET.Dtos.Usuarios;
using ProyectoNET.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProyectoNET.Data;
using ProyectoNET.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;

namespace ProyectoNET.Controllers
{
    //api/Usuario
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _repository;
        private readonly IMapper _mapper;
         private readonly AppSettings _appSettings;

        public UsuarioController(IUsuarioRepo repository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
          _repository = repository;
          _mapper = mapper;
          _appSettings = appSettings.Value;     
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UsuarioAutenticateDto model)
        {
            var user = _repository.Autenticar(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Usuario o password incorrectos" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
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
                Token = tokenString
            });
        }


        //GET api/usuarios
        [HttpGet]
        public ActionResult <IEnumerable<UsuarioReadDto>> GetAllUsuarios()
        {
            var Usuario = _repository.GetAllUsuarios();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(Usuario));
        }

        //GET api/usuarios/{id}
        [HttpGet("{id}", Name ="GetUsuarioById")]
        public ActionResult <UsuarioReadDto> GetUsuarioById(int id)
        {
            var Usuario = _repository.GetUsuarioById(id);
            if(Usuario!=null)
            {
                return Ok(_mapper.Map<UsuarioReadDto>(Usuario));
            }
            return NotFound();
        }

        //POST api/usuarios
        [HttpPost]
        public ActionResult <UsuarioReadDto> CreateUsuario([FromBody]UsuarioCreateDto usuarioCreateDto)
        {
            var UsuarioModel = _mapper.Map<Usuario>(usuarioCreateDto);
           try
           {
                _repository.CreateUsuario(UsuarioModel, usuarioCreateDto.PasswordPlano);
                _repository.SaveChanges();

                var UsuarioReadDto = _mapper.Map<UsuarioReadDto>(UsuarioModel);

                return CreatedAtRoute(nameof(GetUsuarioById), new {Id = UsuarioReadDto.Id}, UsuarioReadDto);
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
            var UsuarioModelFromRepo = _repository.GetUsuarioById(id);
            if(UsuarioModelFromRepo == null){
                return NotFound();
            }
            _mapper.Map(UsuarioUpdateDto, UsuarioModelFromRepo);
            _repository.UpdateUsuario(UsuarioModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/usuarios/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUsuarioUpdtate(int id, JsonPatchDocument<UsuarioUpdateDto> patchDoc)
        {
            var UsuarioModelFromRepo = _repository.GetUsuarioById(id);
            if(UsuarioModelFromRepo == null){
                return NotFound();
            }

            var UsuarioToPatch = _mapper.Map<UsuarioUpdateDto>(UsuarioModelFromRepo);
            patchDoc.ApplyTo(UsuarioToPatch, ModelState);
            if(!TryValidateModel(UsuarioToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(UsuarioToPatch, UsuarioModelFromRepo);
            _repository.UpdateUsuario(UsuarioModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUsuario(int id)
        {
            var UsuarioModelFromRepo = _repository.GetUsuarioById(id);
            if(UsuarioModelFromRepo == null){
                return NotFound();
            }
            _repository.DeleteUsuario(UsuarioModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
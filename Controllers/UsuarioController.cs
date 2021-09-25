using System.Collections.Generic;
using AutoMapper;
using ProyectoNET.Dtos.Usuarios;
using ProyectoNET.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProyectoNET.Data;

namespace ProyectoNET.Controllers
{
    //api/Usuario
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepo _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepo repository, IMapper mapper)
        {
          _repository = repository;
          _mapper = mapper;     
        }

        //GET api/Usuario
        [HttpGet]
        public ActionResult <IEnumerable<UsuarioReadDto>> GetAllUsuarios()
        {
            var Usuario = _repository.GetAllUsuarios();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(Usuario));
        }

        //GET api/Usuario/{id}
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

        //POST api/commands
        [HttpPost]
        public ActionResult <UsuarioReadDto> CreateUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            var UsuarioModel = _mapper.Map<Usuario>(usuarioCreateDto);
            _repository.CreateUsuario(UsuarioModel);
            _repository.SaveChanges();

            var UsuarioReadDto = _mapper.Map<UsuarioReadDto>(UsuarioModel);

            return CreatedAtRoute(nameof(GetUsuarioById), new {Id = UsuarioReadDto.Id}, UsuarioReadDto);
            //return Ok(commandReadDto);
        }

        //PUT api/commands/{id}
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

        //PATCH api/commands/{id}
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

        //DELETE api/commands/{id}
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
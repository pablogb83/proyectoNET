﻿using AutoMapper;
using BusinessLayer.IBL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using DataAccessLayer.Dtos.UsuarioPuerta;
using DataAccessLayer.Dtos.Usuarios;
using DataAccessLayer.Dtos.PuertaAccesos;

namespace NetCoreWebAPI.Controllers
{
    [Route("api/usuarioPuerta")]
    [ApiController]
    public class UsuarioPuertaController : ControllerBase
    {
        private readonly IBL_UsuarioPuerta _bl;
        private readonly IMapper _mapper;

        public UsuarioPuertaController(IBL_UsuarioPuerta bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/usuarioEdificio
        [HttpGet]
        [Authorize(Roles = "ADMIN")]

        public async Task<ActionResult<IEnumerable<UsuarioPuertaReadDto>>> GetAllUsuarioPuerta()
        {
            var usuarioPuerta =await _bl.GetAllUsuarioPuertaAsync();
            return Ok(_mapper.Map<IEnumerable<UsuarioPuertaReadDto>>(usuarioPuerta));
        }

        [Authorize(Roles = "PORTERO")]
        //POST api/usuarioEdificio
        [HttpPost]
        public async Task<ActionResult<UsuarioPuertaReadDto>> CreateUsuarioPuertaAsync(UsuarioPuertaCreateDto usuarioPuertaCreateDto)
        {
            //var usuarioEdificioModel = _mapper.Map<UsuarioEdificio>(UsuarioEdificioCreateDto);
            try
            {
                await _bl.CreateUsuarioPuertaAsync(usuarioPuertaCreateDto.UsuarioId, usuarioPuertaCreateDto.PuertaId);
                _bl.SaveChanges();
                return Ok(new { msg = "Usuario agregado correctamente" });
        
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //GET api/usuarioEdificio/id
        [HttpGet("{id}", Name = "GetUsuarioPuerta")]
        [Authorize(Roles = "ADMIN,PORTERO")]

        public async Task<ActionResult<UsuarioReadDto>> GetUsuarioPuerta(int id)
        {
            try
            {
                var usuario = await _bl.GetUsuarioPuerta(id);
                return Ok(_mapper.Map<UsuarioReadDto>(usuario));
            }
            catch
            {
                return BadRequest("Algo salio mal");
            }

        }

        //DELETE api/usuarios/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]

        public ActionResult DeleteUsuarioPuerta(int id)
        {
            try
            {
                _bl.DeleteUsuarioPuerta(id);
                _bl.SaveChanges();
                return Ok(new { msg = "Puerta liberada correctamente" });
            }
            catch
            {
                return BadRequest("Algo salio mal");
            }


        }

        [HttpGet("puerta/{id}")]
        [Authorize(Roles = "ADMIN,PORTERO")]

        public ActionResult<PuertaReadDto> GetPuertaUsuario(int id)
        {
            var puerta = _bl.GetPuertaUsuario(id);
            return Ok(_mapper.Map<PuertaReadDto>(puerta));
        }
    }
}

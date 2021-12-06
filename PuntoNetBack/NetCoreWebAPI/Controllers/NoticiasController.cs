﻿using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Noticias;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Controllers
{
    //api/noticias
    [Route("api/noticias")]
    [ApiController]
    public class NoticiasController : ControllerBase
    {
        private readonly IBL_Noticias _bl;
        private readonly IMapper _mapper;

        public NoticiasController(IBL_Noticias bl, IMapper mapper)
        {
            _bl = bl;
            _mapper = mapper;
        }

        //GET api/noticias
        [HttpGet]
        public ActionResult<IEnumerable<NoticiaReadDto>> GetAllNoticias()
        {
            var noticias = _bl.GetAllNoticias();
            return Ok(_mapper.Map<IEnumerable<NoticiaReadDto>>(noticias));
        }

        //GET api/noticias/{id}
        [HttpGet("{id}", Name = "GetNoticiaById")]
        public ActionResult<NoticiaReadDto> GetNoticiaById(int id)
        {
            var noticia = _bl.GetNoticiaById(id);
            if (noticia != null)
            {
                return Ok(_mapper.Map<NoticiaReadDto>(noticia));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<NoticiaReadDto> CreateNoticia(NoticiaCreateDto noticiaCreateDto)
        {
            var noticiaModel = _mapper.Map<Noticias>(noticiaCreateDto);
            _bl.CreateNoticia(noticiaModel);
            _bl.SaveChanges();

            var noticiaReadDto = _mapper.Map<NoticiaReadDto>(noticiaModel);

            return CreatedAtRoute(nameof(GetNoticiaById), new { Id = noticiaReadDto.Id }, noticiaReadDto);

        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateNoticia(int id, NoticiaUpdateDto noticiaUpdateDto)
        {
            var noticiaModelFromRepo = _bl.GetNoticiaById(id);
            if (noticiaModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(noticiaUpdateDto, noticiaModelFromRepo);
            _bl.UpdateNoticia(noticiaModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }


        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteNoticia(int id)
        {
            var noticiaModelFromRepo = _bl.GetNoticiaById(id);
            if (noticiaModelFromRepo == null)
            {
                return NotFound();
            }
            _bl.DeleteNoticia(noticiaModelFromRepo);
            _bl.SaveChanges();
            return NoContent();
        }

    }
}
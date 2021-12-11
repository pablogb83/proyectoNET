using AutoMapper;
using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Persona;
using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Persona : IBL_Persona
    {
        private readonly IDAL_Persona _dal;
        private readonly IMapper _mapper;

        public BL_Persona(IDAL_Persona dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }

        public void AltaMasivaPersona(List<PersonaCreateDto> personas)
        {
            var persona = new Persona();
            foreach (var prs in personas)
            {
                persona = _mapper.Map<Persona>(prs);
                persona.PhotoFileName = "anonymous.png";
                if(GetPersonaByDocumento(persona.nro_doc) == null)
                {
                    _dal.CreatePersona(persona);
                    SaveChanges();
                }
            }
        }

        public void CreatePersona(Persona prs)
        {
            _dal.CreatePersona(prs);
        }

        public void DeletePersona(Persona prs)
        {
            _dal.DeletePersona(prs);
        }

        public IEnumerable<Persona> GetAllPersonas()
        {
            return _dal.GetAllPersonas();
        }

        public IEnumerable<Persona> GetAllPersonasBusqueda(string filter)
        {
            return _dal.GetAllPersonasBusqueda(filter);
        }

        public Persona GetPersonaByDocumento(string nro_doc)
        {
            return _dal.GetPersonaByDocumento(nro_doc);
        }

        public Persona GetPersonaById(int Id)
        {
            return _dal.GetPersonaById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdatePersona(Persona prs)
        {
            _dal.UpdatePersona(prs);
        }

    }
}

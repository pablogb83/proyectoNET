using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Persona : IDAL_Persona
    {
        private readonly WebAPIContext _context;

        public DAL_Persona(WebAPIContext context)
        {
            _context = context;
        }

        public void CreatePersona(Persona prs)
        {
            if (prs == null)
            {
                throw new ArgumentNullException(nameof(prs));
            }
            _context.Personas.Add(prs);
        }

        public void DeletePersona(Persona prs)
        {
            if (prs == null)
            {
                throw new ArgumentNullException(nameof(prs));
            }

            _context.Personas.Remove(prs);
        }

        public IEnumerable<Persona> GetAllPersonas()
        {
            return _context.Personas.ToList();
        }

        public IEnumerable<Persona> GetAllPersonasBusqueda(string filter)
        {
            return _context.Personas.Where(p=> 
                (p.Apellidos.Contains(filter)) || 
                (p.Nombres.Contains(filter)) ||
                (p.nro_doc.Contains(filter)) ||
                (p.Email.Contains(filter)) ||
                (p.Telefono.Contains(filter)));
        }

        public Persona GetPersonaByDocumento(string nro_doc)
        {
            return _context.Personas.FirstOrDefault(p => p.nro_doc == nro_doc);
        }

        public Persona GetPersonaById(int Id)
        {
            return _context.Personas.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePersona(Persona prs)
        {
            //nothing
        }
    }
}

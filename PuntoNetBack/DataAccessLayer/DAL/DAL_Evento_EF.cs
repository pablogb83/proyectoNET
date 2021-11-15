using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAL
{
    public class DAL_Evento_EF : IDAL_Evento 
    {
        private readonly WebAPIContext _context;

        public DAL_Evento_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateEvento(Evento evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }
            _context.Eventos.Add(evt);

            SaveChanges();
        }

        public void DeleteEvento(Evento evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            _context.Eventos.Remove(evt);
        }

        public IEnumerable<Evento> GetAllEventos()
        {
            return _context.Eventos.ToList();
        }

        public Evento GetEventoById(int Id) 
        {
            return _context.Eventos.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEvento(Evento evt)
        {
            //nothing
        }
    }
}

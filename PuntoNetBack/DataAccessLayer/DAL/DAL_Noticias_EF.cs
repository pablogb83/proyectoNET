using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAL
{
    public class DAL_Noticias_EF : IDAL_Noticias
    {
        private readonly WebAPIContext _context;
        public DAL_Noticias_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateNoticia(Noticias not)
        {
            if (not == null)
            {
                throw new ArgumentNullException(nameof(not));
            }
            _context.Noticias.Add(not);
        }

        public void DeleteNoticia(Noticias not)
        {
            if (not == null)
            {
                throw new ArgumentNullException(nameof(not));
            }

            _context.Noticias.Remove(not);
        }

        public IEnumerable<Noticias> GetAllNoticias() 
        {
            return _context.Noticias.ToList();

        }

        public IEnumerable<Noticias> GetUltimasNoticias()
        {
            return _context.Noticias.OrderByDescending(not => not.FechaPublicacion).Take(5);
        }

        public Noticias GetNoticiaById(int Id) 
        {
            return _context.Noticias.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateNoticia(Noticias not)
        {
            //nothing
        }

    }
}

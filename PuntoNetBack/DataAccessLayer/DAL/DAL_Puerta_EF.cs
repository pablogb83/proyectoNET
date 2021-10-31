using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Puerta_EF : IDAL_Puerta
    {
        private readonly WebAPIContext _context;

        public DAL_Puerta_EF(WebAPIContext context)
        {
            _context = context;
        }


        public void CreatePuerta(Puerta Puerta)
        {
            _context.Puertas.Add(Puerta);
        }

        public void DeletePuerta(Puerta Puerta)
        {
            _context.Puertas.Remove(Puerta);
        }

        public IEnumerable<Puerta> GetAllPuertas()
        {
            return _context.Puertas.ToList();
        }

        public Puerta GetPuertaById(int idPuertaAcceso)
        {
            return _context.Puertas.FirstOrDefault(p => p.Id == idPuertaAcceso);
        }

        public IEnumerable<Puerta> GetPuertasEdificio(int idEdificio)
        {
            Edificio edi =  _context.Edificios.FirstOrDefault(p => p.Id == idEdificio);
            return edi.puerta_accesos;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePuerta(int idPuertaAcceso)
        {
        }
    }
}
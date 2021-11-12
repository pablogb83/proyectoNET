using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Salon_EF : IDAL_Salon
    {
        private readonly WebAPIContext _context;

        public DAL_Salon_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateSalon(Salon salon)
        {
            _context.Salones.Add(salon);
        }

        public void DeleteSalon(Salon salon)
        {
            _context.Salones.Remove(salon);
        }

        public IEnumerable<Salon> GetAllSalon()
        {
            return _context.Salones.ToList();
        }

        public Salon GetSalonById(int Id)
        {
            return _context.Salones.FirstOrDefault(s => s.Id == Id);
        }

        public IEnumerable<Salon> GetSalonesEdificio(int idEdificio)
        {
            Edificio edi = _context.Edificios.FirstOrDefault(p => p.Id == idEdificio);
            return edi.Salones;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateSalon(Salon salon)
        {
            
        }
    }
}

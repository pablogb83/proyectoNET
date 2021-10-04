using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Edificio_EF : IDAL_Edificio
    {
        private readonly WebAPIContext _context;

        public DAL_Edificio_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateEdificio(Edificio edi)
        {
            if (edi == null)
            {
                throw new ArgumentNullException(nameof(edi));
            }

            _context.Edificios.Add(edi);
        }

        public void DeleteEdificio(Edificio edi)
        {
            if (edi == null)
            {
                throw new ArgumentNullException(nameof(edi));
            }
            _context.Edificios.Remove(edi);
        }

        public IEnumerable<Edificio> GetAllEdificios() //precisa saber que Institucion
        {
            return _context.Edificios.ToList();
        }

        public Edificio GetEdificioById(int Id) //idem, edi + inst
        {
            return _context.Edificios.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEdificio(Edificio edi)
        {
            //nothing
        }
    }
}

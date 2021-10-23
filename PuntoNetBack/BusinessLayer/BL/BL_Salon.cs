using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Salon : IBL_Salon
    {
        private readonly DataAccessLayer.IDAL.IDAL_Salon _dal;
        private readonly DataAccessLayer.IDAL.IDAL_Edificio _dalEdi;

        public BL_Salon(DataAccessLayer.IDAL.IDAL_Salon dal, DataAccessLayer.IDAL.IDAL_Edificio dalEdi)
        {
            _dal = dal;
            _dalEdi = dalEdi;
        }

        public void CreateSalon(Salon salon, int idEdificio)
        {
            Edificio edi = _dalEdi.GetEdificioById(idEdificio);
            salon.edificio = edi;
            _dal.CreateSalon(salon);
        }

        public void DeleteSalon(Salon salon)
        {
            _dal.DeleteSalon(salon);
        }

        public IEnumerable<Salon> GetAllSalon()
        {
            return _dal.GetAllSalon();
        }

        public Salon GetSalonById(int Id)
        {
            return _dal.GetSalonById(Id);
        }

        public IEnumerable<Salon> GetSalonesEdificio(int idEdificio)
        {
            return _dal.GetSalonesEdificio(idEdificio);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateSalon(Salon salon)
        {
            _dal.UpdateSalon(salon);
        }
    }
}

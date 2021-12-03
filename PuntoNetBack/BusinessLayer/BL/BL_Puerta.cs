using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Puerta : IBL_Puerta
    {

        private readonly DataAccessLayer.IDAL.IDAL_Puerta _dal;
        private readonly DataAccessLayer.IDAL.IDAL_Edificio _dalEdi;

        public BL_Puerta(DataAccessLayer.IDAL.IDAL_Puerta dal, DataAccessLayer.IDAL.IDAL_Edificio dalEdi)
        {
            _dal = dal;
            _dalEdi = dalEdi; 
        }

        public void CreatePuerta(Puerta puertaacceso, int idEdificio)
        {
            Edificio edi = _dalEdi.GetEdificioById(idEdificio);
            puertaacceso.edificio = edi;
            _dal.CreatePuerta(puertaacceso);
        }

        public void DeletePuerta(Puerta idPuertaAcceso)
        {
            _dal.DeletePuerta(idPuertaAcceso);
        }

        public IEnumerable<Puerta> GetAllPuertas()
        {
            return _dal.GetAllPuertas();
        }

        public Puerta GetPuertaById(int idPuertaAcceso)
        {
            return _dal.GetPuertaById(idPuertaAcceso);
        }

        public IEnumerable<Puerta> GetPuertasEdificio(int idEdificio)
        {
            return _dal.GetPuertasEdificio(idEdificio);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdatePuerta(int idPuertaAcceso)
        {
            _dal.UpdatePuerta(idPuertaAcceso);
        }
    }
}

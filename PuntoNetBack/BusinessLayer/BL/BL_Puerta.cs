using BusinessLayer.IBL;
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
    public class BL_Puerta : IBL_Puerta
    {

        private readonly IDAL_Puerta _dal;
        private readonly IDAL_Edificio _dalEdi;
        private readonly IDAL_Acceso _dalAcc;

        public BL_Puerta(IDAL_Puerta dal, IDAL_Edificio dalEdi, IDAL_Acceso dalAcc)
        {
            _dal = dal;
            _dalEdi = dalEdi;
            _dalAcc = dalAcc;
        }

        public void CreatePuerta(Puerta puertaacceso, int idEdificio)
        {
            Edificio edi = _dalEdi.GetEdificioById(idEdificio);
            puertaacceso.edificio = edi;
            _dal.CreatePuerta(puertaacceso);
        }

        public void DeletePuerta(Puerta idPuertaAcceso)
        {
            var accesosPuerta = _dalAcc.GetAccesosPuerta(idPuertaAcceso.Id);
            if (accesosPuerta.Any())
            {
                throw new AppException("La puerta ya tiene accesos registrados");
            }
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

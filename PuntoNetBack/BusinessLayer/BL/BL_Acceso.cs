using BusinessLayer.IBL;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Acceso : IBL_Acceso
    {
        private readonly IDAL_Acceso _dal;
        private readonly IDAL_Puerta _dalPta;
        private readonly IDAL_Persona _dalPrs;

        public BL_Acceso(IDAL_Acceso dal, IDAL_Puerta dalPta, IDAL_Persona dalPrs)
        {
            _dal = dal;
            _dalPta = dalPta;
            _dalPrs = dalPrs;
        }

        public void CreateAcceso(Acceso acc, int PersonaId, int PuertaId)
        {
            acc.FechaHora = DateTime.Now;
            Puerta pta = _dalPta.GetPuertaById(PuertaId);
            Persona prs = _dalPrs.GetPersonaById(PersonaId);
            if(pta!=null && prs != null)
            {
                acc.Persona = prs;
                acc.Puerta = pta;
                _dal.CreateAcceso(acc);
            }
        }

        public void DeleteAcceso(Acceso acc)
        {
            _dal.DeleteAcceso(acc);
        }

        public Acceso GetAccesoById(int Id)
        {
            return _dal.GetAccesoById(Id);
        }

        public IEnumerable<Acceso> GetAccesosEdificio(int idEdificio)
        {
            return _dal.GetAccesosEdificio(idEdificio);
        }

        public IEnumerable<Acceso> GetAccesosPersona(int idPersona)
        {
            return _dal.GetAccesosPersona(idPersona);
        }

        public IEnumerable<Acceso> GetAccesosPuerta(int idPuerta)
        {
            return _dal.GetAccesosPuerta(idPuerta);
        }

        public IEnumerable<Acceso> GetAllAccesos()
        {
            return _dal.GetAllAccesos();
        }

        public bool SaveChanges()
        {
           return _dal.SaveChanges();
        }

        public void UpdateAcceso(Acceso acc)
        {
            _dal.UpdateAcceso(acc);
        }
    }
}

using BusinessLayer.IBL;
using DataAccessLayer.Helpers;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Institucion : IBL_Institucion
    {
        private readonly DataAccessLayer.IDAL.IDAL_Institucion _dal;

        public BL_Institucion(DataAccessLayer.IDAL.IDAL_Institucion dal)
        {
            _dal = dal;
        }

        public void CreateInstitucion(Institucion inst)
        {
            _dal.CreateInstitucion(inst);
        }

        public void DeleteInstitucion(Institucion inst)
        {
            _dal.DeleteInstitucion(inst);
        }

        public IEnumerable<Institucion> GetAllInstituciones()
        {
            return _dal.GetAllInstituciones();
        }

        public List<Transaction> GetFacturacion(string insitucionId, DateTime fechainicio, DateTime fechafin)
        {
            return _dal.GetFacturacion(insitucionId, fechainicio, fechafin);
        }

        public Institucion GetInstitucionById(string Id)
        {
            return _dal.GetInstitucionById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateInstitucion(Institucion inst)
        {
            _dal.UpdateInstitucion(inst);
        }

    }
}

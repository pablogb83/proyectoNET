using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Edificio : IBL_Edificio
    {
        private readonly DataAccessLayer.IDAL.IDAL_Edificio _dal;

        public BL_Edificio(DataAccessLayer.IDAL.IDAL_Edificio dal)
        {
            _dal = dal;
        }

        public void CreateEdificio(Edificio edi)
        {
            _dal.CreateEdificio(edi);
        }

        public void DeleteEdificio(Edificio edi)
        {
            _dal.DeleteEdificio(edi);
        }

        public IEnumerable<Edificio> GetAllEdificios()
        {
            return _dal.GetAllEdificios();
        }

        public Edificio GetEdificioById(int Id)
        {
            return _dal.GetEdificioById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateEdificio(Edificio edi)
        {
            _dal.UpdateEdificio(edi);
        }
    }
}

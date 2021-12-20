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
    public class BL_Edificio : IBL_Edificio
    {
        private readonly IDAL_Edificio _dal;
        private readonly IDAL_Puerta _dalPuerta;
        private readonly IDAL_Salon _dalSalon;

        public BL_Edificio(IDAL_Edificio dal,IDAL_Puerta dalPuerta, IDAL_Salon dalSalon)
        {
            _dal = dal;
            _dalPuerta = dalPuerta;
            _dalSalon = dalSalon;
        }

        public void CreateEdificio(Edificio edi)
        {
            _dal.CreateEdificio(edi);
        }

        public void DeleteEdificio(Edificio edi)
        {
            if (_dalPuerta.GetPuertasEdificio(edi.Id).Any())
            {
                throw new AppException("El edificio tiene puertas asignadas");
            }
            if (_dalSalon.GetSalonesEdificio(edi.Id).Any())
            {
                throw new AppException("El edificio tiene salones asignados");
            }
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

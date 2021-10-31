using BusinessLayer.IBL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Evento : IBL_Evento
    {
        private readonly DataAccessLayer.IDAL.IDAL_Evento _dal;

        public BL_Evento(DataAccessLayer.IDAL.IDAL_Evento dal)
        {
            _dal = dal;
        }

        public void CreateEvento(Evento evt)
        {
            _dal.CreateEvento(evt);
        }

        public void DeleteEvento(Evento evt)
        {
            _dal.DeleteEvento(evt);
        }

        public IEnumerable<Evento> GetAllEventos()
        {
            return _dal.GetAllEventos();
        }

        public Evento GetEventoById(int Id)
        {
            return _dal.GetEventoById(Id);
        }

        public bool SaveChanges()
        {
            return _dal.SaveChanges();
        }

        public void UpdateEvento(Evento evt)
        {
            _dal.UpdateEvento(evt);
        }

    }
}

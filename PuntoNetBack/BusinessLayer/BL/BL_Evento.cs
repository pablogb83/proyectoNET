using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Eventos;
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

        public void CreateEventoRecurrente(EventoRecurrenteCreateDto evt)
        {

            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            foreach (DateTime day in EachDay(evt.FechaInicioEvt, evt.FechaFinEvt))
            {
                if (evt.Dias.Contains<int>(((int)day.DayOfWeek)))
                {
                    Evento ev = new Evento();
                    ev.Descripcion = evt.Descripcion;
                    ev.Nombre = evt.Nombre;
                    ev.FechaInicioEvt = day.Date + evt.HoraInicio;
                    ev.FechaFinEvt = ev.FechaInicioEvt.AddHours(evt.Duracion);
                    ev.PhotoFileName = evt.PhotoFileName;
                    _dal.CreateEventoRecurrente(ev);
                } 
            }
            SaveChanges();
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
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

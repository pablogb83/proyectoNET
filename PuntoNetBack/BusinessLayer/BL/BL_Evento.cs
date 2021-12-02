using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Eventos;
using DataAccessLayer.Helpers;
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

        private readonly DataAccessLayer.IDAL.IDAL_Salon _dalSalon;

        public BL_Evento(DataAccessLayer.IDAL.IDAL_Evento dal, DataAccessLayer.IDAL.IDAL_Salon dalSalon)
        {
            _dal = dal;
            _dalSalon = dalSalon;
        }

        public void CreateEvento(Evento evt, int SalonId)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }
            Salon salon =_dalSalon.GetSalonById(SalonId);
            if (salon==null)
            {
                throw new AppException("El salon no existe");

            }
            if (!SalonDisponible(SalonId, evt.FechaInicioEvt, evt.FechaFinEvt))
            {
                throw new AppException("El salon seleccionado esta ocupado en la fecha y hora indicada");
            }
            evt.Salon = salon;
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

        public bool SalonDisponible(int salonId, DateTime fechaInicio, DateTime fechaFin)
        {
            var eventos = _dal.GetEventoSalonFecha(salonId, fechaInicio, fechaFin);
            return (eventos==null || eventos.Count()==0);
        }

        public IEnumerable<Salon> GetSalonesDisponibles(DateTime fechainicio, DateTime fechafin)
        {
            return _dal.GetSalonesDisponibles(fechainicio, fechafin);
        }

    }
}

using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.DAL
{
    public class DAL_Evento_EF : IDAL_Evento 
    {
        private readonly WebAPIContext _context;

        public DAL_Evento_EF(WebAPIContext context)
        {
            _context = context;
        }

        public void CreateEvento(Evento evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            //IEnumerable<DateTime> dates = EachDay(evt.FechaInicioEvt, evt.FechaFinEvt);

            foreach (DateTime day in EachDay(evt.FechaInicioEvt, evt.FechaFinEvt))
            {
                Evento ev = new Evento();
                ev.Descripcion = evt.Descripcion;
                ev.Nombre = evt.Nombre;
                ev.FechaInicioEvt = day.Date;
                ev.FechaFinEvt = ev.FechaInicioEvt.AddHours(5);
                ev.PhotoFileName = evt.PhotoFileName;
                _context.Eventos.Add(ev);
            }
            SaveChanges();

            //_context.Eventos.Add(evt);

            DayOfWeek name = evt.FechaInicioEvt.DayOfWeek;

            //List<DateTime> dates = new List<DateTime>();
            //IEnumerable<DateTime> dates2 = EachDay(evt.FechaInicioEvt, evt.FechaFinEvt);

        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public void DeleteEvento(Evento evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            _context.Eventos.Remove(evt);
        }

        public IEnumerable<Evento> GetAllEventos()
        {
            return _context.Eventos.ToList();
        }

        public Evento GetEventoById(int Id) 
        {
            return _context.Eventos.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateEvento(Evento evt)
        {
            //nothing
        }

        public void CreateEventoRecurrente(Evento evt)
        {
            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            _context.Eventos.Add(evt);
        }
    }
}

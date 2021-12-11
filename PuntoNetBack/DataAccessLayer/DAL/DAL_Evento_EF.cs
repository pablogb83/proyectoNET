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
            _context.Eventos.Add(evt);
            SaveChanges();
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

        public IEnumerable<Evento>  GetEventoSalonFecha(int salonId, DateTime fechainicio, DateTime fechafin)
        {
            return _context.Eventos.Where(ev => ev.Salon.Id == salonId && (ev.FechaInicioEvt <= fechafin && ev.FechaFinEvt >= fechainicio));
        }
        //public IEnumerable<Salon> GetSalonesDisponibles(DateTime fechainicio, DateTime fechafin)
        //{
        //    var salonesOcupados = _context.Eventos.Where(ev => ((ev.FechaInicioEvt <= fechafin && ev.FechaFinEvt >= fechainicio))).Select(ev => ev.Salon.Id).ToArray();
        //    var salonesLibres = _context.Salones.Where(salon => !salonesOcupados.Contains(salon.Id));

        //    return salonesLibres;
        //}

        public IEnumerable<Salon> GetSalonesDisponibles(DateTime fechainicio, DateTime fechafin, int edificioId)
        {
                var salonesOcupados = _context.Eventos.Where(ev => ((ev.FechaInicioEvt <= fechafin && ev.FechaFinEvt >= fechainicio))).Select(ev => ev.Salon.Id).ToArray();
                var salonesLibres = _context.Salones.Where(salon => !salonesOcupados.Contains(salon.Id) && salon.edificio.Id == edificioId);
                return salonesLibres;
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

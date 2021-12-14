using BusinessLayer.IBL;
using DataAccessLayer.Dtos.Eventos;
using DataAccessLayer.Dtos.Salon;
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
    public class BL_Evento : IBL_Evento
    {
        private readonly IDAL_Evento _dal;

        private readonly IDAL_Salon _dalSalon;

        private readonly IDAL_Edificio _dalEdificio;

        private readonly IBL_UsuarioEdificio _blUsrEd;

        public BL_Evento(IDAL_Evento dal, IDAL_Salon dalSalon, IDAL_Edificio dalEdificio, IBL_UsuarioEdificio blUsrEd)
        {
            _dal = dal;
            _dalSalon = dalSalon;
            _dalEdificio = dalEdificio;
            _blUsrEd = blUsrEd;
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

        public void CreateEventoRecurrente(EventoRecurrenteCreateDto evt, int salonId)
        {

            if (evt == null)
            {
                throw new ArgumentNullException(nameof(evt));
            }

            Salon salon = _dalSalon.GetSalonById(salonId);
            if (salon == null)
            {
                throw new AppException("El salon no existe");

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
                    ev.Salon = salon;
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

        public void UpdateEvento(Evento evt, int SalonId)
        {
            Salon salon = _dalSalon.GetSalonById(SalonId);
            if (salon == null)
            {
                throw new AppException("El salon no existe");
            }
            if (evt.Salon.edificio.Id != salon.edificio.Id)
            {
                throw new AppException("El evento debe estar en el mismo edificio");
            }
            if (!SalonDisponibleUpdate(SalonId, evt.FechaInicioEvt, evt.FechaFinEvt,evt.Id))
            {
                throw new AppException("El salon seleccionado esta ocupado en la fecha y hora indicada");
            }
            evt.Salon = salon;
            _dal.UpdateEvento(evt);
        }

        public bool SalonDisponibleUpdate(int salonId, DateTime fechaInicio, DateTime fechaFin, int eventoId)
        {
            var eventos = _dal.GetEventoSalonFecha(salonId, fechaInicio, fechaFin);
            var eventosDistintos =  eventos.Where(ev => ev.Id != eventoId);
            return (eventosDistintos == null || eventosDistintos.Count() == 0);
        }

        public bool SalonDisponible(int salonId, DateTime fechaInicio, DateTime fechaFin)
        {
            var eventos = _dal.GetEventoSalonFecha(salonId, fechaInicio, fechaFin);
            return (eventos==null || eventos.Count()==0);
        }

        public IEnumerable<Salon> GetSalonesDisponibles(SalonesDisponiblesDto datos)
        {
            Boolean primeraVueta = true;
            List<Salon> salonesDisponibles = new List<Salon>();
            List<Salon> salonesParciales = new List<Salon>();
            if (_dalEdificio.GetEdificioById(datos.EdificioId) == null)
            {
                throw new AppException("El edificio no existe");
            }
            if (datos.TipoEvento == "simple")
            {
                salonesDisponibles = _dal.GetSalonesDisponibles(datos.FechaInicioEvt, datos.FechaFinEvt, datos.EdificioId).ToList();
            }
            else
            {
                foreach (DateTime day in EachDay(datos.FechaInicioEvt, datos.FechaFinEvt))
                {
                    if (datos.dias.Contains<int>(((int)day.DayOfWeek)))
                    {
                        var fechaIni = day.Date + datos.HoraInicio;
                        var fechaFin = fechaIni.AddHours(datos.Duracion);
                        if (primeraVueta)
                        {
                            salonesDisponibles = _dal.GetSalonesDisponibles(fechaIni, fechaFin, datos.EdificioId).ToList();
                            primeraVueta = false;
                        }
                        else
                        {
                            salonesParciales = _dal.GetSalonesDisponibles(fechaIni, fechaFin, datos.EdificioId).ToList();
                            if (salonesParciales != null)
                            {
                                List<Salon> noRepetidos = salonesDisponibles.Except(salonesParciales).ToList();
                                salonesDisponibles = salonesDisponibles.Except(noRepetidos).ToList();
                            }
                        }
                    }
                }
            }
         
            return salonesDisponibles;
     
        }

        public IEnumerable<Evento> GetAllEventosEdificio(int idedificio)
        {
            var edificio = _dalEdificio.GetEdificioById(idedificio);
            if (edificio==null)
            {
                throw new AppException("El edificio ingresado no existe");
            }
            return _dal.GetAllEventosEdificio(idedificio);
        }

        public async Task<bool> VerificarEventoGestor(int salonId,int idUsuario)
        {
            var edificioUsuario = await _blUsrEd.GetEdificioUsuario(idUsuario);
            if (edificioUsuario == null)
            {
                throw new AppException("El usuario no tiene edificio asignado");
            }
            var salon = _dalSalon.GetSalonById(salonId);
            if (salon == null)
            {
                throw new AppException("El salon no existe");
            }
            return (salon.edificio.Id == edificioUsuario.Id);
        }
    }
}

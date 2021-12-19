using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Evento
    {
        bool SaveChanges();
        IEnumerable<Evento> GetAllEventos();
        IEnumerable<Evento> GetAllEventosEdificio(int idedificio);
        Evento GetEventoById(int Id);
        void CreateEvento(Evento evt);
        void UpdateEvento(Evento evt);
        void DeleteEvento(Evento evt);
        void CreateEventoRecurrente(Evento evt);
        IEnumerable<Evento> GetEventoSalonFecha(int salonId, DateTime fechainicio, DateTime fechafin);
        IEnumerable<Salon> GetSalonesDisponibles(DateTime fechainicio, DateTime fechafin, int edificioId);
    }
}

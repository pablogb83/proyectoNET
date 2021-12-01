using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Evento
    {
        bool SaveChanges();
        IEnumerable<Evento> GetAllEventos();
        Evento GetEventoById(int Id);
        void CreateEvento(Evento evt);
        void UpdateEvento(Evento evt);
        void DeleteEvento(Evento evt);
        void CreateEventoRecurrente(Evento evt);
        IEnumerable<Evento> GetEventoSalonFecha(int salonId, DateTime fechainicio, DateTime fechafin);
    }
}

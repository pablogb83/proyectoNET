using DataAccessLayer.Dtos.Eventos;
using DataAccessLayer.Dtos.Salon;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Evento
    {
        bool SaveChanges();
        IEnumerable<Evento> GetAllEventos();
        IEnumerable<Evento> GetAllEventosEdificio(int idedificio);
        Evento GetEventoById(int Id);
        void CreateEvento(Evento evt, int SalonId);
        void UpdateEvento(Evento evt, int SalonId);
        void DeleteEvento(Evento evt);
        void CreateEventoRecurrente(EventoRecurrenteCreateDto evt, int SalonId);
        IEnumerable<Salon> GetSalonesDisponibles(SalonesDisponiblesDto datos);
        Task<bool> VerificarEventoGestor(int salonId, int idUsuario);

    }
}

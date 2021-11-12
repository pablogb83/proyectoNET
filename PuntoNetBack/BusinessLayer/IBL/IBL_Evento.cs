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
        Evento GetEventoById(int Id);
        void CreateEvento(Evento evt);
        void UpdateEvento(Evento evt);
        void DeleteEvento(Evento evt);
    }
}

using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Acceso
    {
        bool SaveChanges();

        IEnumerable<Acceso> GetAllAccesos();
        IEnumerable<Acceso> GetAccesosEdificio(int idEdificio);
        IEnumerable<Acceso> GetAccesosPuerta(int idPuerta);
        IEnumerable<Acceso> GetAccesosPersona(int idPersona);
        Acceso GetAccesoById(int Id);
        void CreateAcceso(Acceso acc, int PesonaId, int PuertaId);
        void UpdateAcceso(Acceso acc);
        void DeleteAcceso(Acceso acc);
    }
}

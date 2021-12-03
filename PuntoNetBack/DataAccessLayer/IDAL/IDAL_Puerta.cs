using System;
using System.Collections.Generic;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Puerta
    {
        bool SaveChanges();
        IEnumerable<Puerta> GetAllPuertas();
        Puerta GetPuertaById(int idPuertaAcceso);
        IEnumerable<Puerta> GetPuertasEdificio(int idEdificio);
        void CreatePuerta(Puerta puertaacceso);
        void UpdatePuerta(int idPuertaAcceso);
        void DeletePuerta(Puerta idPuertaAcceso);

    }
}

using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Puerta
    {
        bool SaveChanges();
        IEnumerable<Puerta> GetAllPuertas();
        Puerta GetPuertaById(int idPuertaAcceso);
        IEnumerable<Puerta> GetPuertasEdificio(int idEdificio);
        void CreatePuerta(Puerta puertaacceso, int idEdificio);
        void UpdatePuerta(int idPuertaAcceso);
        void DeletePuerta(Puerta idPuertaAcceso);
    }
}

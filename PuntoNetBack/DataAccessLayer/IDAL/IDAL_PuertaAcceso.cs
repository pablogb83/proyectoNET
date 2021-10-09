using System;
using System.Collections.Generic;
using Shared.ModeloDeDominio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_PuertaAcceso
    {
        bool SaveChanges();
        IEnumerable<PuertaAcceso> GetAllPuertasAcceso(int idEdificio);
        PuertaAcceso GetAccesoById(int idEdificio, int idPuertaAcceso);
        void CreatePuertaAcceso(int idEdificio, PuertaAcceso puertaacceso);
        void UpdatePuertaAcceso(int idEdificio, int idPuertaAcceso);
        void DeletePuertaAcceso(int idEdificio, int idPuertaAcceso);
        
    }
}

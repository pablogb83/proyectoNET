using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.ModeloDeDominio;

namespace BusinessLayer.IBL
{
    public interface IBL_PuertaAcceso
    {
        bool SaveChanges();

        IEnumerable<PuertaAcceso> GetPuertaAccesos(int idEdificio);
        PuertaAcceso GetInstitucionById(int Id);
        void CreateInstitucion(PuertaAcceso pta_acc);
        void UpdateInstitucion(PuertaAcceso pta_acc);
        void DeleteInstitucion(PuertaAcceso pta_acc);
    }
}

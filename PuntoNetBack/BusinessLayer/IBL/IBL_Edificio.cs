using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Edificio
    {
        bool SaveChanges();

        IEnumerable<Edificio> GetAllEdificios(); //falta decirle la inst ..
        Edificio GetEdificioById(int Id);
        void CreateEdificio(Edificio edi);
        void UpdateEdificio(Edificio edi);
        void DeleteEdificio(Edificio edi);
    }
}

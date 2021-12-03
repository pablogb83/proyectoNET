using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Edificio
    {
        bool SaveChanges();

        IEnumerable<Edificio> GetAllEdificios();
        Edificio GetEdificioById(int Id);
        void CreateEdificio(Edificio edi);
        void UpdateEdificio(Edificio edi);
        void DeleteEdificio(Edificio edi);
    }
}

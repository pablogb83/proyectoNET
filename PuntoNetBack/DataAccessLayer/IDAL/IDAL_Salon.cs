using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Salon
    {
        bool SaveChanges();

        IEnumerable<Salon> GetAllSalon();
        Salon GetSalonById(int Id);
        IEnumerable<Salon> GetSalonesEdificio(int idEdificio);
        void CreateSalon(Salon salon);
        void UpdateSalon(Salon salon);
        void DeleteSalon(Salon salon);
    }
}

using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Institucion
    {
        bool SaveChanges();

        IEnumerable<Institucion> GetAllInstituciones();
        Institucion GetInstitucionById(int Id);
        void CreateInstitucion(Institucion inst);
        void UpdateInstitucion(Institucion inst);
        void DeleteInstitucion(Institucion inst);
    }
}

using DataAccessLayer.Helpers;
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
        Institucion GetInstitucionById(string Id);
        IEnumerable<Institucion> GetInstitucionesProducto(string planId);
        void CreateInstitucion(Institucion inst);
        void UpdateInstitucion(Institucion inst);
        Task UpdateInstitucionAzure(Institucion inst, string nombreViejo);
        void DeleteInstitucion(Institucion inst);
        List<Transaction> GetFacturacion(string insitucionId, DateTime fechainicio, DateTime fechafin);
    }
}

using System.Collections.Generic;
using ProyectoNET.Models;

namespace ProyectoNET.Data
{
    public interface IInstitucionRepo
    {
        bool SaveChanges();

        IEnumerable<Institucion> GetAllInstituciones();
        Institucion GetInstitucionById(int Id);
        void CreateInstitucion(Institucion inst);
        void UpdateInstitucion(Institucion inst);
        void DeleteInstitucion(Institucion inst);

    }
}
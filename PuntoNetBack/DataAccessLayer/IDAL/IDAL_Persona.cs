using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Persona
    {
        bool SaveChanges();

        IEnumerable<Persona> GetAllPersonas();
        Persona GetPersonaById(int Id);
        void CreatePersona(Persona prs);
        void UpdatePersona(Persona prs);
        void DeletePersona(Persona prs);
        IEnumerable<Persona> GetAllPersonasBusqueda(string filter);
    }
}

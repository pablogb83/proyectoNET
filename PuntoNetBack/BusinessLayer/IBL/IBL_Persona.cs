using DataAccessLayer.Dtos.Persona;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Persona
    {
        bool SaveChanges();

        IEnumerable<Persona> GetAllPersonas();
        Persona GetPersonaById(int Id);
        void CreatePersona(Persona prs);
        void UpdatePersona(Persona prs);
        void DeletePersona(Persona prs);
        void AltaMasivaPersona(List<PersonaCreateDto> personas);
    }
}

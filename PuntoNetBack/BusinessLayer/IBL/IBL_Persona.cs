using DataAccessLayer.Dtos.Persona;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.IO;
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
        Persona GetPersonaByDocumento(string nro_doc);

        void CreatePersona(Persona prs);
        Task CreatePersonaConFoto(Persona prs, Stream stream, string tenantName);
        Task UpdatePersona(Persona prs, string documentoViejo, string tenant);
        Task UpdatePersonaConFoto(string documentoViejo, string documentoNuevo, Stream imagen, string tenant);
        void DeletePersona(Persona prs);
        void AltaMasivaPersona(List<PersonaCreateDto> personas);
        IEnumerable<Persona> GetAllPersonasBusqueda(string filter);
    }
}

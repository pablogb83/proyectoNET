using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Persona : IDAL_Persona
    {
        private readonly WebAPIContext _context;

        public DAL_Persona(WebAPIContext context)
        {
            _context = context;
        }

        public void CreatePersona(Persona prs)
        {
            if (prs == null)
            {
                throw new ArgumentNullException(nameof(prs));
            }
            _context.Personas.Add(prs);
 
        }

        public async Task CreatePersonaConFoto(Persona prs, Stream stream, string tenantName)
        {
            if (prs == null)
            {
                throw new ArgumentNullException(nameof(prs));
            }
            _context.Personas.Add(prs);
            await DAL_FaceApi.AgregarPersona(prs.nro_doc, stream, tenantName);
            _context.SaveChanges();
        }

        public void DeletePersona(Persona prs)
        {
            if (prs == null)
            {
                throw new ArgumentNullException(nameof(prs));
            }

            _context.Personas.Remove(prs);
        }

        public IEnumerable<Persona> GetAllPersonas()
        {
            return _context.Personas.ToList();
        }

        public IEnumerable<Persona> GetAllPersonasBusqueda(string filter)
        {
            return _context.Personas.Where(p=> 
                (p.Apellidos.Contains(filter)) || 
                (p.Nombres.Contains(filter)) ||
                (p.nro_doc.Contains(filter)) ||
                (p.Email.Contains(filter)) ||
                (p.Telefono.Contains(filter)));
        }

        public Persona GetPersonaByDocumento(string nro_doc)
        {
            return _context.Personas.FirstOrDefault(p => p.nro_doc == nro_doc);
        }

        public Persona GetPersonaById(int Id)
        {
            return _context.Personas.FirstOrDefault(p => p.Id == Id);
        }

        public bool SaveChanges()
        {
            try
            {
                return (_context.SaveChanges() >= 0);
            }
            catch(Exception e)
            {
                throw new AppException(e.Message);
            }
            
        }

        public async Task UpdatePersona(Persona prs, string documentoViejo,string tenant)
        {
            await DAL_FaceApi.actualizarDocumentoAzure(documentoViejo, prs.nro_doc, tenant);
        }

        public async Task UpdatePersonaConFoto(string documentoViejo, string documentoNuevo, Stream imagen, string tenant)
        {
            //nothing
            await DAL_FaceApi.ActualizarPersona(documentoViejo, documentoNuevo, tenant, imagen);
        }
    }
}

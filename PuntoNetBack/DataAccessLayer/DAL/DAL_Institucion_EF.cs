using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Institucion_EF : IDAL_Institucion
    {
        private readonly MultiTenantStoreDbContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IDAL_FaceApi _dalFace;


        public DAL_Institucion_EF(MultiTenantStoreDbContext context, IHttpClientFactory clientFactory, IDAL_FaceApi dalFace)
        {
            _context = context;
            _clientFactory = clientFactory;
            _dalFace = dalFace;
        }

        public void CreateInstitucion(Institucion inst)
        {
            if (inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            _context.Instituciones.Add(inst);
            _context.SaveChanges();
            try
            {
                //DAL_FaceApi.DeletePersonGroup(inst).GetAwaiter();
                _dalFace.CreatePersonGroup(inst).Wait();
            }
            catch (Exception e)
            {

            }
        }

        public void DeleteInstitucion(Institucion inst)
        {
            if (inst == null)
            {
                throw new ArgumentNullException(nameof(inst));
            }
            _context.Instituciones.Remove(inst);
        }

        public IEnumerable<Institucion> GetAllInstituciones()
        {
            return _context.Instituciones.Where(inst => inst.Name!="Puertan").ToList();
        }

        public Institucion GetInstitucionById(string Id)
        {
            return _context.Instituciones.FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Institucion> GetInstitucionesProducto(string planId)
        {
            return _context.Instituciones.Where(x => x.PlanId == planId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public List<Transaction> GetFacturacion(string insitucionId, DateTime fechainicio, DateTime fechafin)
        {
  
            try
            {
                var paypalTools = new PaypalUtil(_clientFactory);
                string token = paypalTools.getPayPalAccessToken();
                var inst = _context.Instituciones.FirstOrDefault(p => p.Id == insitucionId);
                if (inst.Suscripcion == null)
                {
                    throw new AppException("La institucion aun no tiene facturas");
                }
                return paypalTools.getFacturasSuscripcion(token, inst.Suscripcion.Id, fechainicio, fechafin);
            }
            catch(Exception e)
            {
                return new List<Transaction>(); 
            }
            
        }

        public void UpdateInstitucion(Institucion inst)
        {
            //nothing
        }

        public async Task UpdateInstitucionAzure(Institucion inst, string nombreViejo)
        {
            await _dalFace.ActualizarInstitucion(nombreViejo, inst.Name);
        }
    }
}

using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DataAccessLayer.DAL
{
    public class DAL_Institucion_EF : IDAL_Institucion
    {
        private readonly MultiTenantStoreDbContext _context;
        private readonly IHttpClientFactory _clientFactory;


        public DAL_Institucion_EF(MultiTenantStoreDbContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
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
                DAL_FaceApi.DeletePersonGroup(inst.Name.ToLower()).Wait();
                DAL_FaceApi.CreatePersonGroup(inst.Name.ToLower()).Wait();

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
    }
}

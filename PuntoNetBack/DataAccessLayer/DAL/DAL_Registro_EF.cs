using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Microsoft.AspNetCore.Http;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DAL_Registro_EF : IDAL_Registro
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly MultiTenantStoreDbContext _context;

        public DAL_Registro_EF(IHttpClientFactory clientFactory, MultiTenantStoreDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }

        public bool AuthorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body)
        {
            var paypal = new PaypalUtil(_clientFactory);
            string token = paypal.getPayPalAccessToken();
            bool auth =paypal.authorizePayment(headers,body, token);
            if (auth && body.event_type.Equals("BILLING.SUBSCRIPTION.ACTIVATED"))
            {
                Institucion inst =  _context.Instituciones.FirstOrDefault(p => p.Id == body.resource.custom_id);
                inst.Activa = true;
                if(inst.Suscripcion == null)
                {
                    var suscripcion = new Suscripcion();
                    suscripcion.Id = body.resource.id;
                    suscripcion.estado = body.resource.status;
                    inst.Suscripcion = suscripcion;
                }
                _context.SaveChanges();
            }
            return auth;
        }
    } 
       
}

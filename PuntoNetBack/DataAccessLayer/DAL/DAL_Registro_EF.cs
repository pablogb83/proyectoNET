using DataAccessLayer.Helpers;
using DataAccessLayer.IDAL;
using Microsoft.AspNetCore.Http;
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

        public DAL_Registro_EF(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public string CrearSuscripcionAsync()
        {
            var paypal = new PaypalUtil(_clientFactory);
            string token = paypal.getPayPalAccessToken();
            string link = paypal.createSuscription(token);
            return link;
        }

        public bool AuthorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body)
        {
            var paypal = new PaypalUtil(_clientFactory);
            string token = paypal.getPayPalAccessToken();
            bool auth =paypal.authorizePayment(headers,body, token);
            return auth;
        }
    } 
       
}

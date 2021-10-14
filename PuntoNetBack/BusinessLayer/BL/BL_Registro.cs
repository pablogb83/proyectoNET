using BusinessLayer.IBL;
using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.BL
{
    public class BL_Registro : IBL_Registro
    {
        private readonly DataAccessLayer.IDAL.IDAL_Registro _dal;

        public BL_Registro(DataAccessLayer.IDAL.IDAL_Registro dal)
        {
            _dal = dal;
        }

        public string CrearSuscripcion()
        {
            return _dal.CrearSuscripcionAsync();
        }
        public bool AuthorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body)
        {
            return _dal.AuthorizePayment(headers, body);
        }
    }
}

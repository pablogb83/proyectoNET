using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_Registro
    {
        string CrearSuscripcionAsync(string inst);
        bool AuthorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body);

    }

}

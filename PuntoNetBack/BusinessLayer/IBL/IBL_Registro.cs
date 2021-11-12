using DataAccessLayer.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_Registro
    {
        bool AuthorizePayment(IHeaderDictionary headers, PaypalSuscriptionActivated body);
    }
}

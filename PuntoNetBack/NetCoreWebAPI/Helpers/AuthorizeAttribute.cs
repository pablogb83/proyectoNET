using DataAccessLayer.IDAL;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string user = context.HttpContext.Items["User"].ToString();
            var tenantInfo = context.HttpContext.GetMultiTenantContext<Institucion>().TenantInfo;
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {

                var userService = context.HttpContext.RequestServices.GetRequiredService<IDAL_Usuario>();
                int userId = int.Parse(user);
                context.HttpContext.Items["UserData"] = userService.GetUsuarioById(userId);
                Console.WriteLine(userId);
            }
        }
    }
}

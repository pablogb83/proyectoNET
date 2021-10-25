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
        public string Role;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            int user = (int)context.HttpContext.Items["User"];
            if (user==0)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                var userService = context.HttpContext.RequestServices.GetRequiredService<IDAL_Usuario>();
                var userData = userService.GetUsuarioById(user);
                if (userData.Role.NombreRol.Equals(this.Role))
                {
                    context.HttpContext.Items["UserData"] = userData;
                }
                else
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    return;
                }
            }
        }
    }
}

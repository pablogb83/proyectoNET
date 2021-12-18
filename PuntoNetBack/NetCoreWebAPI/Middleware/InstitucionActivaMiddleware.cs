using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.ModeloDeDominio;
using Finbuckle.MultiTenant;
using System.Threading.Tasks;
using DataAccessLayer.Helpers;
using System.Linq;

namespace NetCoreWebAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class InstitucionActivaMiddleware
    {
        private readonly RequestDelegate _next;

        public InstitucionActivaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            if (IsPublic(httpContext.Request.Path))
            {
                return _next(httpContext);
            }
            var role ="";
            if (httpContext.User.Claims.Any())
            {
                role = httpContext.User.Claims.Skip(2).FirstOrDefault().Value;
            }
            var tenantActual = httpContext.GetMultiTenantContext<Institucion>();
            if (tenantActual != null && AllowInactive(httpContext.Request.Path))
            {
                return _next(httpContext);
            }
            if ((tenantActual == null && !role.Equals("SUPERADMIN"))||(!tenantActual.TenantInfo.Activa && !role.Equals("SUPERADMIN")))
            {
                throw new AppException("Institucion invalida o inactiva, asegurese de estar logueado");
            }
            return _next(httpContext);
        }
        private bool IsPublic(string path)
        {
            string[] publicDomains = { "authenticate", "Photos", "registro/confirm", "publicas","instituciones" };
            return publicDomains.FirstOrDefault(x => path.Contains(x)) != null;
        }

        private bool AllowInactive(string path)
        {
            string[] publicDomains = { "active", "instituciones/admin","productos" };
            return publicDomains.FirstOrDefault(x => path.Contains(x)) != null;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InstitucionActivaMiddleware>();
        }
    }
}

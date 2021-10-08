using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;


namespace NetCoreWebAPI.Middleware
{
    public class TenantInfoMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantInfoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }
    }
}

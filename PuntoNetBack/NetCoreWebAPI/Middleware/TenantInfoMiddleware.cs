using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Shared.ModeloDeDominio;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var tenantInfo = context.RequestServices.GetRequiredService<TenantInfo>();
            var tenantId = context.Request.Headers["Tenant"];

            if (!string.IsNullOrEmpty(tenantId))
            {
                tenantInfo.Id = Int32.Parse(tenantId);


                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
            else
            {
                //responder algo
            }

        }
    }
}

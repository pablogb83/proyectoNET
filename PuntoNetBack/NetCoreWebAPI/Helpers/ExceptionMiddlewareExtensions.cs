using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NetCoreWebAPI.Helpers;
using System.Net;
namespace GlobalErrorHandling.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var Error = contextFeature.Error.GetType();
                    if (contextFeature != null &&  Error.Name.Equals("AppException"))
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            message = contextFeature.Error.Message
                        }.ToString());
                    }
                    else
                    {
                        if (contextFeature != null)
                        {
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                message = "Algo salio mal"
                            }.ToString());
                        }
                    }
                });
            });
        }

     
    }
}
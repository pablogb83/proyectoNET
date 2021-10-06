using Microsoft.Extensions.Configuration;
using DataAccessLayer;
using DataAccessLayer.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.ModeloDeDominio;

namespace NetCoreWebAPI.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseDiscriminatorColumn(this IServiceCollection services, IConfiguration configuration)
         => services.UseEFInterceptor<DiscriminatorColumnInterceptor>(configuration);

        private static IServiceCollection UseEFInterceptor<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IInterceptor
        {
            return services
                .AddScoped<DbContextOptions>((serviceProvider) =>
                {
                    var tenant = serviceProvider.GetRequiredService<TenantInfo>();

                    var efServices = new ServiceCollection();
                    efServices.AddEntityFrameworkSqlServer();
                    efServices.AddScoped<TenantInfo>(s =>
                        serviceProvider.GetRequiredService<TenantInfo>()); // Allows DI for tenant info, set by parent pipeline via middleware
                    efServices.AddScoped<IInterceptor, T>(); // Adds the interceptor

                    string connectionString = configuration.GetConnectionString("CommanderConnection");

                    return new DbContextOptionsBuilder<WebAPIContext>()
                        .UseInternalServiceProvider(efServices.BuildServiceProvider())
                        .UseSqlServer(connectionString)
                        .Options;
                })
                .AddScoped(s => new WebAPIContext(s.GetRequiredService<DbContextOptions>()));
        }

    }
}

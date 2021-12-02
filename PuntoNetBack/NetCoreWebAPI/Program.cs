using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //   CreateHostBuilder(args).Build().Run();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)//LogEventLevel.Information
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.MongoDB(databaseUrl: "mongodb+srv://mongoNet:1234@cluster0.h9fxq.mongodb.net/myFirstDatabase?retryWrites=true&w=majority", collectionName: "Logs")
                .CreateLogger();
            try
            {
                Log.Information(messageTemplate: "Starting web host");
                CreateHostBuilder(args).Build().Run();
                //CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, messageTemplate: "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        /*public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartUp<Startup>();*/
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    
                });
    }
}
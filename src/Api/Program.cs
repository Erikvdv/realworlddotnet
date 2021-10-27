using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Realworlddotnet.Infrastructure.Extensions.Logging;
using Serilog;
using LoggerConfigurationExtensions = Realworlddotnet.Infrastructure.Extensions.Logging.LoggerConfigurationExtensions;

namespace Realworlddotnet.Api
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            LoggerConfigurationExtensions
                .SetupLoggerConfiguration("realworld_dotnet");

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
                Thread.Sleep(2000);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
                {
                    loggerConfiguration.ConfigureBaseLogging("realworld_dotnet");
                    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}

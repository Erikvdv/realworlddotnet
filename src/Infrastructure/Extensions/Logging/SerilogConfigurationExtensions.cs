using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Realworlddotnet.Infrastructure.Extensions.Logging;

public static class SerilogConfigurationExtensions
{
    public static LoggerConfiguration ConfigureBaseLogging(
        this LoggerConfiguration loggerConfiguration,
        string appName)
    {
        loggerConfiguration.MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .WriteTo.Async(a => a.Console())
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("ApplicationName", appName);
        return loggerConfiguration;
    }

    public static LoggerConfiguration AddApplicationInsightsLogging(
        this LoggerConfiguration loggerConfiguration,
        IServiceProvider services,
        IConfiguration configuration)
    {
        var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
        var authenticationApiKey = configuration.GetValue<string>("ApplicationInsights:AuthenticationApiKey");

        if (string.IsNullOrWhiteSpace(instrumentationKey))
        {
            return loggerConfiguration;
        }

        var config = TelemetryConfiguration.CreateDefault();
        config.InstrumentationKey = instrumentationKey;

        if (!string.IsNullOrWhiteSpace(authenticationApiKey))
        {
            QuickPulseTelemetryProcessor? quickPulseProcessor = null;
            config.DefaultTelemetrySink.TelemetryProcessorChainBuilder.Use(next =>
                {
                    quickPulseProcessor = new QuickPulseTelemetryProcessor(next);
                    return quickPulseProcessor;
                })
                .Build();
            var quickPulse = new QuickPulseTelemetryModule { AuthenticationApiKey = authenticationApiKey };
            quickPulse.Initialize(config);

            quickPulse.RegisterTelemetryProcessor(quickPulseProcessor);
        }

        TelemetryClient client = new(config);
        loggerConfiguration.WriteTo.ApplicationInsights(
            client,
            TelemetryConverter.Traces);

        return loggerConfiguration;
    }
}

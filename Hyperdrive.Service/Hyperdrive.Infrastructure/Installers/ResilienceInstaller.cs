using Azure.Monitor.OpenTelemetry.AspNetCore;
using Hyperdrive.Infrastructure.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ServiceDiscovery;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System;

namespace Hyperdrive.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ResilienceInstaller" /> class.
/// </summary>
public static class ResilienceInstaller
{
    private const string HealthEndpointPath = "/health";
    private const string AlivenessEndpointPath = "/alive";

    /// <summary>
    /// Installs Aspire Services
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder"/></param>
    /// <returns>Instance of <see cref="IHostApplicationBuilder"/></returns>
    public static IHostApplicationBuilder InstallAspireServices(this IHostApplicationBuilder builder)
    {
        builder.InstallOpenTelemetry();

        builder.InstallDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        builder.Services.Configure<ServiceDiscoveryOptions>(options => { options.AllowedSchemes = ["https"]; });

        return builder;
    }

    /// <summary>
    /// Installs Open Telemetry
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder"/></param>
    /// <returns>Instance of <see cref="IHostApplicationBuilder"/></returns>
    private static IHostApplicationBuilder InstallOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter();
            })
            .WithTracing(tracing =>
            {                
                tracing.AddAspNetCoreInstrumentation(tracing =>
                            // Exclude health check requests from tracing
                            tracing.Filter = context =>
                                !context.Request.Path.StartsWithSegments(HealthEndpointPath)
                                && !context.Request.Path.StartsWithSegments(AlivenessEndpointPath)
                        )
                    .AddHttpClientInstrumentation();
            });

        builder.InstallOpenTelemetryExporters();

        return builder;
    }

    /// <summary>
    /// Installs Open Telemetry Exporters
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder"/></param>
    /// <returns>Instance of <see cref="IHostApplicationBuilder"/></returns>
    private static IHostApplicationBuilder InstallOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter) builder.Services.AddOpenTelemetry().UseOtlpExporter();

        var useAzureMonitor = !string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

        if (useAzureMonitor) builder.Services.AddOpenTelemetry().UseAzureMonitor();

        return builder;
    }

    /// <summary>
    /// Installs Open Telemetry Exporters
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder"/></param>
    /// <returns>Instance of <see cref="IHostApplicationBuilder"/></returns>
    private static IHostApplicationBuilder InstallDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        builder.Services.AddRequestTimeouts(static timeouts =>
            timeouts.AddPolicy("HealthChecks", TimeSpan.FromSeconds(5)));

        builder.Services.AddOutputCache(static caching =>
            caching.AddPolicy("HealthChecks", static policy => policy.Expire(TimeSpan.FromSeconds(10))));

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationContext>()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    /// <summary>
    /// Uses Default Health Endpoints
    /// </summary>
    /// <param name="app">Injected <see cref="WebApplication"/></param>
    /// <returns>Instance of <see cref="WebApplication"/></returns>
    public static WebApplication UseDefaultHealthEndpoints(this WebApplication app)
    {
        app.MapGroup("").CacheOutput("HealthChecks").WithRequestTimeout("HealthChecks");

        // All health checks must pass for app to be considered ready to accept traffic after starting
        app.MapHealthChecks(HealthEndpointPath);

        // Only health checks tagged with the "live" tag must pass for app to be considered alive
        app.MapHealthChecks(AlivenessEndpointPath, new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live")
        });

        return app;
    }

    /// <summary>
    /// Uses Default Prometheus Scraping Endpoint
    /// </summary>
    /// <param name="app">Injected <see cref="WebApplication"/></param>
    /// <returns>Instance of <see cref="WebApplication"/></returns>
    public static WebApplication UseDefaultPrometheusScrapingEndpoint(this WebApplication app)
    {
        app.MapPrometheusScrapingEndpoint();

        return app;
    }

}
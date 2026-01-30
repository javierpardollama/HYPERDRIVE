using Hyperdrive.Ai.Application.Installers;
using Hyperdrive.Ai.Infrastructure.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var @builder = WebApplication.CreateBuilder(args);

@builder.Configuration.AddEnvironmentVariables();

var @apiSettings = @builder.InstallApiSetttings();
var @aiSettings = @builder.InstallOpenAiSettings();
var @rateSettings = @builder.InstallRateLimitSettings();

@builder.Services.InstallEntityFramework(@builder.Configuration);
@builder.Services.InstallOpenAi(@aiSettings);

@builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

@builder.Services.InstallApiVersions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
@builder.Services.InstallOpenApi();

// Register the service and implementation for the database context
@builder.Services.InstallManagers();
@builder.Services.InstallMediatR();

// Register the Mvc services to the services container
@builder.Services.AddResponseCaching();

// Add customized Authentication to the services container.
@builder.Services.InstallCors(@apiSettings);

@builder.Services.InstallProblemDetails();
@builder.Services.InstallRateLimiter(@rateSettings);

@builder.InstallAspireServices();

@builder.InstallSecureApi();

var @app = @builder.Build();

// Learn more about configuring app pipeline at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0
// 1. Problem details (error handling)
@app.UseProblemDetails();

// 2. Routing
@app.UseRouting();

// 3. OpenAPI (Swagger UI)
@app.UseOpenApi();

// 4. Apply migrations (usually early, before requests)
@app.UseMigrations();

// 5. CORS (must be before endpoints)
@app.UseCors();

// 6. Security headers
@app.UseSecureApi();

// 7. Identification & custom middlewares
@app.UseMiddlewares();

// 8. Performance features
@app.UseResponseCaching();
@app.UseRateLimiter();
@app.UseRequestTimeouts();
@app.UseOutputCache();

// 9. Health endpoints
@app.UseDefaultHealthEndpoints();

// 10. Endpoint execution
@app.MapControllers();

@app.Run();
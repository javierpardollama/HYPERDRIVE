using Hyperdrive.Application.Installers;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var @builder = WebApplication.CreateBuilder(args);

// Add services to the container.

@builder.Services.InstallEntityFramework(builder.Configuration);

@builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

builder.Services.InstallApiVersions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
@builder.Services.InstallOpenApi();

@builder.Services.InstallManagers();
@builder.Services.InstallMediatR();

@builder.Services.AddResponseCaching();

// Register the Jwt Settings to the configuration container.
var @jwtSettings = new JwtSettings();
@builder.Configuration.GetSection("Jwt").Bind(@jwtSettings);
@builder.Services.Configure<JwtSettings>(@builder.Configuration.GetSection("Jwt"));

@builder.Services.InstallIdentification(@jwtSettings);
@builder.Services.InstallCors(@jwtSettings);

// Register the Rate Limit Settings to the configuration container.
var @rateSettings = new RateLimitSettings();
@builder.Configuration.GetSection("RateLimit").Bind(@rateSettings);
@builder.Services.Configure<RateLimitSettings>(@builder.Configuration.GetSection("RateLimit"));

// Return the Problem Details format for non-successful responses
@builder.Services.InstallProblemDetails();
@builder.Services.InstallRateLimiter(@rateSettings);

@builder.InstallAspireServices();

@builder.InstallSecureApi();

var @app = @builder.Build();

// Learn more about configuring app pipeline at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0
@app.UseOpenApi();

@app.UseMigrations();

@app.UseMiddlewares();

@app.UseSecureApi();

@app.UseCors();

@app.UseIdentification();

@app.UseResponseCaching();

@app.UseRateLimiter();

@app.MapControllers();

@app.UseDefaultHealthEndpoints();

@app.UseRequestTimeouts();
@app.UseOutputCache();

// Return the body of the response when the status code is not successful (the default behavior is to return an empty body with a Status Code)
@app.UseProblemDetails();

@app.Run();

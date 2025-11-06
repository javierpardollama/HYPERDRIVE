using Hyperdrive.Application.Installers;
using Hyperdrive.Infrastructure.Installers;
using Microsoft.AspNetCore.Builder;
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

var @jwtSettings = @builder.InstallJwtSetttings();
var @rateSettings = @builder.InstallRateLimitSettings();

@builder.Services.InstallIdentification(@jwtSettings);
@builder.Services.InstallCors(@jwtSettings);

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

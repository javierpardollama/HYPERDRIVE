using System.Text.Json.Serialization;
using Hyperdrive.Application.Installers;
using Hyperdrive.Domain.Settings;
using Hyperdrive.Infrastructure.Installers;
using Hyperdrive.Tier.Resilience.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var @builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.InstallEntityFramework(builder.Configuration);

@builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.InstallOpenApi();

builder.Services.InstallManagers();
builder.Services.InstallMediatR();

@builder.Services.AddResponseCaching();

// Register the Jwt Settings to the configuration container.
var @JwtSettings = new JwtSettings();
@builder.Configuration.GetSection("Jwt").Bind(@JwtSettings);
@builder.Services.Configure<JwtSettings>(@builder.Configuration.GetSection("Jwt"));

@builder.Services.InstallAuthentication(@JwtSettings);
builder.Services.InstallCors(JwtSettings);

// Register the Rate Limit Settings to the configuration container.
var @RateSettings = new RateLimitSettings();
@builder.Configuration.GetSection("RateLimit").Bind(@RateSettings);
@builder.Services.Configure<RateLimitSettings>(@builder.Configuration.GetSection("RateLimit"));

builder.Services.InstallProblemDetails();
builder.Services.InstallRateLimiter(RateSettings);

@builder.InstallAspireServices();

// Return the Problem Details format for non-successful responses
@builder.Services.AddProblemDetails();

var @app = @builder.Build();

// Configure the HTTP request pipeline.
if (@app.Environment.IsDevelopment())
{
    @app.UseSwagger();
    @app.UseSwaggerUI();
}

app.InstallMigrations();

app.InstallMiddlewares();

@app.UseHttpsRedirection();

// Learn more about configuring app pipeline at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0
@app.UseCors();

@app.UseAuthentication();
@app.UseAuthorization();

@app.UseResponseCaching();

@app.UseRateLimiter();

@app.MapControllers();

@app.MapDefaultHealthEndpoints();

@app.UseRequestTimeouts();
@app.UseOutputCache();

// Return the body of the response when the status code is not successful (the default behavior is to return an empty body with a Status Code)
@app.UseExceptionHandler();
@app.UseStatusCodePages();

@app.Run();

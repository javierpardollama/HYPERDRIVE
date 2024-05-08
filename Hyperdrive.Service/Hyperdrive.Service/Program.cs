using System;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

using Hyperdrive.Service.Extensions;
using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Mappings.Classes;
using Hyperdrive.Tier.Service.Extensions;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

var @builder = WebApplication.CreateBuilder(args);

// Add services to the container.

@builder.Services.AddDbContext<ApplicationContext>(options =>
             options.UseSqlite(@builder.Configuration.GetConnectionString("DefaultConnection")));

@builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

@builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
@builder.Services.AddEndpointsApiExplorer();
@builder.Services.AddCustomizedSwagger();

// Register the Mapping Profiles
@builder.Services.AddAutoMapper(typeof(ModelingProfile).Assembly);

// Register the service and implementation for the database context
@builder.Services.AddCustomizedContexts();

// Register the Mvc services to the services container
@builder.Services.AddCustomizedServices();

@builder.Services.AddResponseCaching();

// Register the Jwt Settings to the configuration container.
var @settings = new JwtSettings();
@builder.Configuration.GetSection("Jwt").Bind(@settings);
@builder.Services.Configure<JwtSettings>(@builder.Configuration.GetSection("Jwt"));


// Add customized Authentication to the services container.
@builder.Services.AddCustomizedAuthentication(@settings);

@builder.Services.AddCustomizedCrossOriginRequests(@settings);

@builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration));

@builder.Services.AddHealthChecks();

@builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));


var @app = @builder.Build();

// Configure the HTTP request pipeline.
if (@app.Environment.IsDevelopment())
{
    @app.UseSwagger();
    @app.UseSwaggerUI();

    @app.UseMigrations();
}

@app.UseCustomizedMiddlewares();

@app.UseHttpsRedirection();

// UseCors must be called before UseResponseCaching, UseAuthentication, UseAuthorization
@app.UseCors();

@app.UseAuthentication();
@app.UseAuthorization();

@app.UseResponseCaching();

@app.UseRateLimiter();

@app.MapControllers();

@app.MapHealthChecks("/healthz");

@app.Run();

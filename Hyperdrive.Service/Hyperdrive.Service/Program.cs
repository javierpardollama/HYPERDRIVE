using Hyperdrive.Service.Extensions;
using Hyperdrive.Tier.Contexts.Classes;
using Hyperdrive.Tier.Entities.Classes;
using Hyperdrive.Tier.Mappings.Classes;
using Hyperdrive.Tier.Service.Extensions;
using Hyperdrive.Tier.Settings.Classes;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Sandwitch.Tier.Contexts.Interceptors;

using System.Text.Json.Serialization;

var @builder = WebApplication.CreateBuilder(args);

// Add services to the container.

@builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.AddInterceptors(new SoftDeleteInterceptor());
    options.UseSqlite(@builder.Configuration.GetConnectionString("DefaultConnection"));
});

@builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

@builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
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
var @JwtSettings = new JwtSettings();
@builder.Configuration.GetSection("Jwt").Bind(@JwtSettings);
@builder.Services.Configure<JwtSettings>(@builder.Configuration.GetSection("Jwt"));

// Add customized Authentication to the services container.
@builder.Services.AddCustomizedAuthentication(@JwtSettings);

@builder.Services.AddCustomizedCrossOriginRequests(@JwtSettings);

// Register the Rate Limit Settings to the configuration container.
var @RateSettings = new RateLimitSettings();
@builder.Configuration.GetSection("RateLimit").Bind(@RateSettings);
@builder.Services.Configure<RateLimitSettings>(@builder.Configuration.GetSection("RateLimit"));

@builder.Services.AddCustomizedRateLimiter(@RateSettings);

@builder.AddCustomizedAspireServices();

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

// UseCors() must be called before UseResponseCaching(), UseAuthentication(), UseAuthorization().
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

@app.Run();

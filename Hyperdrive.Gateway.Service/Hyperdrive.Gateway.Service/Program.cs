using Hyperdrive.Gateway.Application.Installers;
using Hyperdrive.Gateway.Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var @jwtSettings = @builder.InstallJwtSetttings();

@builder.Services.AddResponseCaching();
@builder.Services.InstallIdentification(@jwtSettings);
@builder.Services.InstallCors(@jwtSettings);
@builder.Services.InstallProblemDetails();
@builder.InstallAspireServices();
@builder.InstallSecureApi();
@builder.InstallApiGateway();

var @app = @builder.Build();

// Learn more about configuring app pipeline at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0

// 1. Problem details (error handling)
@app.UseProblemDetails();

// 2. Routing
@app.UseRouting();

// 3. CORS (must be before endpoints)
@app.UseCors();

// 4. Security headers
@app.UseSecureApi();

// 5. Identification & custom middlewares
@app.UseIdentification();
@app.UseMiddlewares();

// 6. Health endpoints
@app.UseDefaultHealthEndpoints();

// 7. Endpoint execution
@app.MapControllers();

// 8. Ocelot
await app.UseApiGateway();

await @app.RunAsync();
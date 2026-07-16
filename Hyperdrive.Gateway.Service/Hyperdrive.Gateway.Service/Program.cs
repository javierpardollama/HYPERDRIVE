using Hyperdrive.Gateway.Application.Installers;
using Hyperdrive.Gateway.Infrastructure.Installers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

var @jwtSettings = @builder.InstallJwtSetttings();

@builder.Services.InstallSerializer();
@builder.InstallOpenApi();
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

// 3. OpenAPI (Swagger UI)
@app.UseOpenApi();

// 4. CORS (must be before endpoints)
@app.UseCors();

// 5. Security headers
@app.UseSecureApi();

// 6. Identification & custom middlewares
@app.UseIdentification();
@app.UseMiddlewares();

// 7. Performance features
@app.UseResponseCaching();
@app.UseRequestTimeouts();
@app.UseOutputCache();

// 8. Health endpoints
@app.UseDefaultHealthEndpoints();

// 9. Endpoint execution
@app.MapControllers();

// 10. Ocelot
await app.UseApiGateway();

@app.Run();
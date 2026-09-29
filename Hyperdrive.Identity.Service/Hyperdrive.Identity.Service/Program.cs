using Hyperdrive.Identity.Application.Installers;
using Hyperdrive.Identity.Infrastructure.Installers;
using Hyperdrive.Identity.Service.Installers;

var @builder = WebApplication.CreateBuilder(args);

@builder.Configuration.AddEnvironmentVariables();

var @jwtSettings = @builder.InstallJwtSetttings();
var @rateSettings = @builder.InstallRateLimitSettings();

@builder.InstallEntityFramework(builder.Configuration);
@builder.InstallSerializer();
@builder.InstallApiVersions();
@builder.Services.InstallOpenApi();
@builder.Services.InstallManagers();
@builder.InstallMediatR();
@builder.Services.AddResponseCaching();
@builder.InstallIdentification(@jwtSettings);
@builder.InstallCors(@jwtSettings);
@builder.InstallProblemDetails();
@builder.InstallRateLimiter(@rateSettings);
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
@app.UseIdentification();
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

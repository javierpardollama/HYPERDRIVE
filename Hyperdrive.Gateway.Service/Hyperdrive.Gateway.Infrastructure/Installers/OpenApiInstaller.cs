using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperdrive.Gateway.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="OpenApiInstaller" /> class.
/// </summary>
public static class OpenApiInstaller
{
    /// <summary>
    ///     Installs Open Api
    /// </summary>
    /// <param name="this">Injected <see cref="IHostApplicationBuilder" /></param>
    public static void InstallOpenApi(this IHostApplicationBuilder @this)
    {
        @this.Services.AddSwaggerForOcelot(@this.Configuration);
    }

    /// <summary>
    ///     Uses Open Api
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static void UseOpenApi(this WebApplication @this)
    {
        if (@this.Environment.IsDevelopment())
        {
            @this.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
        }
    }
}
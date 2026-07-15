using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Hyperdrive.Gateway.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ApiGatewayInstaller" /> class.
/// </summary>
public static class ApiGatewayInstaller
{
    /// <summary>
    ///     Installs Ocelot Api Gateway
    /// </summary>
    /// <param name="this">Injected <see cref="IHostApplicationBuilder" /></param>
    public static void InstallApiGateway(this IHostApplicationBuilder @this)
    {
        @this.Configuration
            .SetBasePath(@this.Environment.ContentRootPath)
            .AddOcelot();
        
        @this.Services
            .AddOcelot(@this.Configuration);
    }
    
    /// <summary>
    ///     Uses Ocelot Api Gateway
    /// </summary>
    /// <param name="this">Injected <see cref="WebApplication" /></param>
    public static async Task UseApiGateway(this WebApplication @this)
    {
        await @this.UseOcelot();
    }
}
using Hyperdrive.Ai.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="CorsInstaller" /> class.
/// </summary>
public static class CorsInstaller
{
    /// <summary>
    ///     Installs Cors
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="ApiSettings" /></param>
    public static void InstallCors(this IServiceCollection @this, ApiSettings @settings)
    {
        @this.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins([.. @settings.ApiAudiences])
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build();
            });
        });
    }
}
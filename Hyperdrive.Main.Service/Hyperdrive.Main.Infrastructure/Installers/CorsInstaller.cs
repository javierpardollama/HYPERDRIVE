using Hyperdrive.Main.Domain.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Main.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="CorsInstaller" /> class.
/// </summary>
public static class CorsInstaller
{
    /// <summary>
    ///     Installs Cors
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="JwtSettings" /></param>
    public static void InstallCors(this IServiceCollection @this, JwtSettings @settings)
    {
        @this.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins([.. @settings.JwtAudiences])
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build();
            });
        });
    }
}
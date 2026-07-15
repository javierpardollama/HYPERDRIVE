using Hyperdrive.Gateway.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperdrive.Gateway.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ConfigurationInstaller" /> class.
/// </summary>
public static class ConfigurationInstaller
{
    /// <summary>
    ///     Installs Jwt Settings
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder" /></param>
    public static JwtSettings InstallJwtSetttings(this IHostApplicationBuilder @builder)
    {
        var @jwtSettings = new JwtSettings();

        @builder.Configuration.GetSection("Jwt").Bind(@jwtSettings);
        @builder.Services.Configure<JwtSettings>(@builder.Configuration.GetSection("Jwt"));

        return @jwtSettings;
    }
}
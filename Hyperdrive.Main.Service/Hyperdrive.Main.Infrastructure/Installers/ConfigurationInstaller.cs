using Hyperdrive.Main.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperdrive.Main.Infrastructure.Installers;

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

    /// <summary>
    ///     Installs Rate Limit Settings
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder" /></param>
    public static RateLimitSettings InstallRateLimitSettings(this IHostApplicationBuilder builder)
    {
        var @rateSettings = new RateLimitSettings();

        @builder.Configuration.GetSection("RateLimit").Bind(@rateSettings);
        @builder.Services.Configure<RateLimitSettings>(@builder.Configuration.GetSection("RateLimit"));

        return @rateSettings;
    }
}
using Hyperdrive.Ai.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="ConfigurationInstaller" /> class.
/// </summary>
public static class ConfigurationInstaller
{
    /// <summary>
    ///     Installs Api Settings
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder" /></param>
    public static ApiSettings InstallApiSetttings(this IHostApplicationBuilder @builder)
    {
        var @jwtSettings = new ApiSettings();

        @builder.Configuration.GetSection("Api").Bind(@jwtSettings);
        @builder.Services.Configure<ApiSettings>(@builder.Configuration.GetSection("Api"));

        return @jwtSettings;
    }

    /// <summary>
    ///     Installs Open Ai Settings
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder" /></param>
    public static OpenAiSettings InstallOpenAiSettings(this IHostApplicationBuilder @builder)
    {
        var @aiSettings = new OpenAiSettings();

        @builder.Configuration.GetSection("OpenAi").Bind(@aiSettings);
        @builder.Services.Configure<OpenAiSettings>(@builder.Configuration.GetSection("OpenAi"));

        return @aiSettings;
    }

    /// <summary>
    ///     Installs Rabbit Settings
    /// </summary>
    /// <param name="builder">Injected <see cref="IHostApplicationBuilder" /></param>
    public static RabbitSettings InstallRabbitSetttings(this IHostApplicationBuilder @builder)
    {
        var @rabbitSettings = new RabbitSettings();

        @builder.Configuration.GetSection("Rabbit").Bind(@rabbitSettings);
        @builder.Services.Configure<RabbitSettings>(@builder.Configuration.GetSection("Rabbit"));

        return @rabbitSettings;
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
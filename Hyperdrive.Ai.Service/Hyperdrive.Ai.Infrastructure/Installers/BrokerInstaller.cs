using Hyperdrive.Ai.Domain.Settings;
using Hyperdrive.Ai.Infrastructure.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Ai.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="BrokerInstaller" /> class.
/// </summary>
public static class BrokerInstaller
{
    /// <summary>
    ///     Installs Rabbit
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    /// <param name="settings">Injected <see cref="RabbitSettings" /></param>
    public static void InstallRabbit(this IServiceCollection @this, RabbitSettings @settings)
    {
        @this.AddHostedService<RabbitBroker>();
    }
}

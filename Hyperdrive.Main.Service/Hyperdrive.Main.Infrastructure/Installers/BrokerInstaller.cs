using Hyperdrive.Main.Domain.Brokers;
using Hyperdrive.Main.Infrastructure.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace Hyperdrive.Main.Infrastructure.Installers;

/// <summary>
///     Represents a <see cref="BrokerInstaller" /> class.
/// </summary>
public static class BrokerInstaller
{
    /// <summary>
    ///     Installs Rabbit
    /// </summary>
    /// <param name="this">Injected <see cref="IServiceCollection" /></param>
    public static void InstallRabbit(this IServiceCollection @this)
    {
        @this.AddSingleton<IRabbitBroker, RabbitBroker>();
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Domain.Brokers;

/// <summary>
/// Represents a <see cref="IRabbitBroker"/> interface.
/// </summary>
public interface IRabbitBroker
{
    /// <summary>
    /// Publishes Messages
    /// </summary>
    /// <param name="message">Injected <see cref="string"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task Publish(string message, CancellationToken cancellationToken = default);
}

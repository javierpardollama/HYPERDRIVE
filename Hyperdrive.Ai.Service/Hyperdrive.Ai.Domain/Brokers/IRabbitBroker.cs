using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Domain.Brokers;

/// <summary>
/// Represents a <see cref="IRabbitBroker"/> interface.
/// </summary>
public interface IRabbitBroker
{
    /// <summary>
    /// Consumes Messages
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task Consume(CancellationToken cancellationToken = default);
}

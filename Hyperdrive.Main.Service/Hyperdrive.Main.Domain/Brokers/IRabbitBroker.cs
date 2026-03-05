using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Domain.Brokers;

/// <summary>
/// Represents a <see cref="IRabbitBroker{T}"/> interface. Inherits<see cref="IAsyncDisposable"/>
/// </summary>
public interface IRabbitBroker<T> : IAsyncDisposable
{
    /// <summary>
    /// Connects
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>   
    public Task ConnectAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes Messages
    /// </summary>
    /// <param name="message">Injected <see cref="T"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public Task Publish(T message, CancellationToken cancellationToken = default);
}

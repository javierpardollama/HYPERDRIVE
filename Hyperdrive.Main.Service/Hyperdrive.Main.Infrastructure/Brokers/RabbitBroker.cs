using Hyperdrive.Main.Domain.Brokers;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Domain.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Main.Infrastructure.Brokers;

/// <summary>
/// Represents a <see cref="RabbitBroker"/> class. Implements <see cref="IRabbitBroker"/>
/// </summary>
/// <param name="connection">Injected <see cref="IConnection"/></param>
/// <param name="settings">Injected <see cref="IOptions{RabbitSettings}"/></param>
public class RabbitBroker(IConnection connection, IOptions<RabbitSettings> settings) : IRabbitBroker
{
    private readonly IConnection _connection = connection;
    private readonly IOptions<RabbitSettings> _settings = settings;

    /// <summary>
    /// Publishes Messages
    /// </summary>
    /// <param name="message">Injected <see cref="string"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Publish(string message, CancellationToken cancellationToken = default)
    {
        var channelOptions = new CreateChannelOptions(
             publisherConfirmationsEnabled: _settings.Value.PublisherConfirmationsEnabled,
             publisherConfirmationTrackingEnabled: _settings.Value.PublisherConfirmationTrackingEnabled
         );

        await using var channel = await _connection.CreateChannelAsync(channelOptions, cancellationToken);

        await channel.QueueDeclareAsync(queue: _settings.Value.Queue,
                                        durable: _settings.Value.Durable,
                                        exclusive: _settings.Value.Exclusive,
                                        autoDelete: _settings.Value.AutoDelete,
                                        cancellationToken: cancellationToken);

        var props = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent
        };

        var body = Encoding.UTF8.GetBytes(message);

        try
        {
            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: _settings.Value.Key,
                mandatory: _settings.Value.Mandatory,
                body: body,
                basicProperties: props,
                cancellationToken: cancellationToken
            );
        }
        catch (AlreadyClosedException ex)
        {
            throw new BrokerException("Channel/Connection closed.", ex);
        }
        catch (OperationInterruptedException ex)
        {
            throw new BrokerException("Broker did not confirm.", ex);
        }
    }
}
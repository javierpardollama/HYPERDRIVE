using Hyperdrive.Main.Domain.Brokers;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Domain.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
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
    private readonly IConnection Connection = connection;
    private readonly IOptions<RabbitSettings> Settings = settings;

    /// <summary>
    /// Publishes Messages
    /// </summary>
    /// <param name="message">Injected <see cref="string"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Publish(string message, CancellationToken cancellationToken = default)
    {
        var channelOptions = new CreateChannelOptions(publisherConfirmationsEnabled: Settings.Value.PublisherConfirmationsEnabled,
                                                      publisherConfirmationTrackingEnabled: Settings.Value.PublisherConfirmationTrackingEnabled);

        await using var channel = await Connection.CreateChannelAsync(channelOptions,
                                                                      cancellationToken);

        await channel.QueueDeclareAsync(queue: Settings.Value.Queue,
                                        durable: Settings.Value.Durable,
                                        exclusive: Settings.Value.Exclusive,
                                        autoDelete: Settings.Value.AutoDelete,
                                        cancellationToken: cancellationToken);

        var props = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent
        };

        var body = Encoding.UTF8.GetBytes(message);

        try
        {
            await channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: Settings.Value.Key,
                                            mandatory: Settings.Value.Mandatory,
                                            body: body,
                                            basicProperties: props,
                                            cancellationToken: cancellationToken);
        }
        catch (PublishException ex)
        {
            throw new BrokerException("Message not send.", ex);
        }
        catch (AlreadyClosedException ex)
        {
            throw new BrokerException("Channel/Connection closed.", ex);
        }
        catch (OperationInterruptedException ex)
        {
            throw new BrokerException("Broker did not confirm.", ex);
        }
        catch (OperationCanceledException ex)
        {
            throw new BrokerException("Message cancelled.", ex);
        }
        catch (SemaphoreFullException ex)
        {
            throw new BrokerException("Threads exceded.", ex);
        }

    }
}
using Hyperdrive.Ai.Domain.Brokers;
using Hyperdrive.Ai.Domain.Exceptions;
using Hyperdrive.Ai.Domain.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hyperdrive.Ai.Infrastructure.Brokers;

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
    /// Consumes Messages
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Consume(CancellationToken cancellationToken = default)
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

        try
        {
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: Settings.Value.Queue,
                                            autoAck: true,
                                            consumer: consumer,
                                            cancellationToken: cancellationToken);

        }
        catch (AlreadyClosedException ex)
        {
            throw new BrokerException("Channel/Connection closed.", ex);
        }
        catch (OperationInterruptedException ex)
        {
            throw new BrokerException("Broker did not confirm.", ex);
        }
        catch (TimeoutException ex)
        {
            throw new BrokerException("Broker did not respond within channel's continuation timeout.", ex);
        }
        catch (ObjectDisposedException ex)
        {
            throw new BrokerException("Channel/Broker disposed.", ex);
        }

    }
}
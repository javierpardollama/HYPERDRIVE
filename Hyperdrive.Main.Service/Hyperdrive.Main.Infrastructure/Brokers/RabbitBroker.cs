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
public class RabbitBroker : IRabbitBroker
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;
    private readonly IOptions<RabbitSettings> _settings;

    /// <summary>
    /// Initializes a new Instance of <see cref="RabbitBroker"/>
    /// </summary>
    /// <param name="settings">Injected <see cref="IOptions{RabbitSettings}"/></param>
    public RabbitBroker(IOptions<RabbitSettings> settings)
    {
        _settings = settings;
        _factory = new ConnectionFactory
        {
            HostName = settings.Value.Url,
            UserName = settings.Value.User,
            Password = settings.Value.Key,
            AutomaticRecoveryEnabled = settings.Value.AutomaticRecoveryEnabled,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(settings.Value.NetworkRecoveryInterval)
        };
    }

    /// <summary>
    /// Connects
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>   
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true })
            return;

        _connection = await _factory.CreateConnectionAsync(cancellationToken);

        var channelOptions = new CreateChannelOptions(publisherConfirmationsEnabled: _settings.Value.PublisherConfirmationsEnabled,
                                                     publisherConfirmationTrackingEnabled: _settings.Value.PublisherConfirmationTrackingEnabled);

        _channel = await _connection.CreateChannelAsync(channelOptions,
                                                      cancellationToken);

        await _channel.QueueDeclareAsync(queue: _settings.Value.Queue,
                                        durable: _settings.Value.Durable,
                                        exclusive: _settings.Value.Exclusive,
                                        autoDelete: _settings.Value.AutoDelete,
                                        cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Publishes Messages
    /// </summary>
    /// <param name="message">Injected <see cref="string"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Publish(string message, CancellationToken cancellationToken = default)
    {
        var props = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent
        };

        var body = Encoding.UTF8.GetBytes(message);

        try
        {
            await ConnectAsync(cancellationToken);

            await _channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: _settings.Value.Key,
                                            mandatory: _settings.Value.Mandatory,
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

    /// <summary>
    /// Disposes see <see cref="RabbitBroker"/>
    /// </summary>
    /// <returns>Instance of <see cref="ValueTask"/></returns>
    public async ValueTask DisposeAsync()
    {
        await _channel.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
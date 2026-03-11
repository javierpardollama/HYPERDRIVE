using Hyperdrive.Main.Domain.Brokers;
using Hyperdrive.Main.Domain.Exceptions;
using Hyperdrive.Main.Domain.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace Hyperdrive.Main.Infrastructure.Brokers;

/// <summary>
/// Represents a <see cref="RabbitBroker{T}"/> class. Implements <see cref="IRabbitBroker{T}"/>
/// </summary>
public class RabbitBroker<T> : IRabbitBroker<T>
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
            HostName = _settings.Value.Url,
            UserName = _settings.Value.User,
            Password = _settings.Value.Key,
            AutomaticRecoveryEnabled = _settings.Value.AutomaticRecoveryEnabled,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(_settings.Value.NetworkRecoveryInterval)
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
    /// <param name="message">Injected <see cref="T"/></param>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task Publish(T message, CancellationToken cancellationToken = default)
    {
        var props = new BasicProperties
        {
            DeliveryMode = DeliveryModes.Persistent
        };

        try
        {
            var messagejson = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(messagejson);

            await ConnectAsync(cancellationToken);

            await _channel.BasicPublishAsync(exchange: string.Empty,
                                            routingKey: _settings.Value.Key,
                                            mandatory: _settings.Value.Mandatory,
                                            body: body,
                                            basicProperties: props,
                                            cancellationToken: cancellationToken);
        }

        catch (NotSupportedException ex)
        {
            throw new BrokerException("Message contained unsupported types.", ex);
        }
        catch (JsonException ex)
        {
            throw new BrokerException("Message contained malformed JSON.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new BrokerException("Invalid JSON Serializer options.", ex);
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
    /// Disposes <see cref="RabbitBroker{T}"/>
    /// </summary>
    /// <returns>Instance of <see cref="ValueTask"/></returns>
    public async ValueTask DisposeAsync()
    {
        await _channel.CloseAsync();
        await _connection.CloseAsync();
        await _channel.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
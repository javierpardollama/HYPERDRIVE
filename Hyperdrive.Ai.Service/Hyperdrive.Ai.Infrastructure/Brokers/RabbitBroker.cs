using Hyperdrive.Ai.Domain.Brokers;
using Hyperdrive.Ai.Domain.Exceptions;
using Hyperdrive.Ai.Domain.Settings;
using Microsoft.Extensions.Hosting;
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
/// Represents a <see cref="RabbitBroker"/> class. Implements <see cref="IHostedService"/>, <see cref="IRabbitBroker"/>
/// </summary>
public class RabbitBroker : IHostedService, IRabbitBroker
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;
    private readonly IOptions<RabbitSettings> _settings;

    /// <summary>
    /// Initializes a new Instane of <see cref="RabbitBroker"/>
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
    /// Starts Consuming Messages
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await ConnectAsync(cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                return Task.CompletedTask;
            };

            await _channel.BasicConsumeAsync(queue: _settings.Value.Queue,
                                            autoAck: false,
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

    /// <summary>
    /// Stops Consuming Messages
    /// </summary>
    /// <param name="cancellationToken">Injected <see cref="CancellationToken"/></param>
    /// <returns>Instance of <see cref="Task"/></returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _channel.CloseAsync(cancellationToken);
        await _connection.CloseAsync(cancellationToken);
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
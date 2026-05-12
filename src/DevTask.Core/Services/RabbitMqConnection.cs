using DevTask.Core.Models.Options.RabbitMq;
using DevTask.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DevTask.Core.Services
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly ConnectionFactory _connectionFactory;

        private IConnection? _connection;
        private IChannel? _channel;

        public RabbitMqConnection(IOptions<RabbitMqConfig> options)
        {
            var config = options.Value;

            _connectionFactory = new ConnectionFactory
            {
                HostName = config.Host,
                Port = config.Port,
                UserName = config.Username,
                Password = config.Password,
            };
        }

        public async Task<IChannel> GetChannelAsync(CancellationToken cancellationToken = default)
        {
            _connection ??= await _connectionFactory.CreateConnectionAsync(cancellationToken);

            var channelOptions = new CreateChannelOptions(publisherConfirmationsEnabled: true, publisherConfirmationTrackingEnabled: true);

            _channel ??= await _connection.CreateChannelAsync(channelOptions, cancellationToken);

            return _channel;
        }

        public async ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                await _channel.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.DisposeAsync();
            }
        }
    }
}

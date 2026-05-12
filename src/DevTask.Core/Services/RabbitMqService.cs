using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using DevTask.Core.Models.Messages;
using DevTask.Core.Services.Interfaces;
using RabbitMQ.Client;

namespace DevTask.Core.Services
{
    internal sealed class RabbitMqService : IRabbitMqService, IAsyncDisposable
    {
        private readonly IRabbitMqConnection _rabbitMqConnection;

        private readonly string _exchangeName;

        private int _activePublishers = 0;
        private bool _isStopping = false;

        private IChannel? _channel;

        public RabbitMqService(IRabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;

            //TODO move it to settings
            _exchangeName = "task.events";
        }

        public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : IMetadataDefinition
        {
            if (_isStopping)
            {
                return;
            }

            try
            {
                Interlocked.Increment(ref _activePublishers);

                _channel ??= await _rabbitMqConnection.GetChannelAsync(cancellationToken);

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                var prop = new BasicProperties
                {
                    ContentType = MediaTypeNames.Application.Json,
                    MessageId = Guid.NewGuid().ToString(),
                    Persistent = true,
                };

                await _channel.BasicPublishAsync(
                    exchange: _exchangeName,
                    routingKey: message.GetEventCode(),
                    mandatory: true,
                    basicProperties: prop,
                    body: body,
                    cancellationToken: cancellationToken);
            }
            finally
            {
                Interlocked.Decrement(ref _activePublishers);
            }
        }

        public async ValueTask DisposeAsync()
        {
            _isStopping = true;

            var waitingTimeout = TimeSpan.FromSeconds(5);
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed <= waitingTimeout)
            {
                await Task.Delay(100);
            }

            if (_channel != null)
            {
                await _channel.DisposeAsync();
            }

            await _rabbitMqConnection.DisposeAsync();
        }
    }
}

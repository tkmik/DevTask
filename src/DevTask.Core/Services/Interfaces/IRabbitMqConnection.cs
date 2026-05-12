using RabbitMQ.Client;

namespace DevTask.Core.Services.Interfaces
{
    public interface IRabbitMqConnection : IAsyncDisposable
    {
        Task<IChannel> GetChannelAsync(CancellationToken cancellationToken = default);
    }
}

using DevTask.Core.Models.Messages;

namespace DevTask.Core.Services.Interfaces
{
    public interface IRabbitMqService
    {
        Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : IMetadataDefinition;
    }
}

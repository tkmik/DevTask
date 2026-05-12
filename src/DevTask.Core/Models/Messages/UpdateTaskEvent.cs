using DevTask.Core.Models.Messages;

namespace DevTask.Core.Models.Events
{
    public record UpdateTaskEvent(Guid TaskId, string Title, DateTimeOffset CompletedAt, string Priority) : IMetadataDefinition
    {
        public string GetEventCode()
        {
            return Consts.UpdateTaskEventCode;
        }
    }
}

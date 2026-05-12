using System.ComponentModel.DataAnnotations;

namespace DevTask.Core.Models.Entity
{
    public sealed class TaskItem : Entity<Guid>
    {
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public PriorityType Priority { get; set; }

        [Timestamp]
        public uint RowVersion { get; set; } = default!;
    }
}

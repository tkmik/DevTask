using DevTask.Core.Models.Entity;

namespace DevTask.Core.Database.Interfaces
{
    public interface ITaskRepository : IEntityFrameworkRepoistory<TaskItem, Guid>
    {
    }
}

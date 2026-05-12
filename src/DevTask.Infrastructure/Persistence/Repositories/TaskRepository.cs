using DevTask.Core.Database;
using DevTask.Core.Database.Interfaces;
using DevTask.Core.Models.Entity;
using DevTask.Infrastructure.Persistence.DbContexts;

namespace DevTask.Infrastructure.Persistence.Repositories
{
    internal sealed class TaskRepository(AppDbContext ctx)
        : EntityFrameworkRepository<AppDbContext, TaskItem, Guid>(ctx), ITaskRepository
    {
    }
}

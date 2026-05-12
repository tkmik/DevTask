using System.Runtime.InteropServices;
using DevTask.Core.Models.Dto;
using FluentResults;

namespace DevTask.Core.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Result<Guid>> AddAsync(CreateTask task, CancellationToken cancellationToken = default);
        Task<Result<IEnumerable<TaskDto>>> GetAsync(CancellationToken cancellationToken = default);
        Task<Result> CompleteAsync(CompleteTask task, CancellationToken cancellationToken = default);
        Task<Result> DeleteAsync(DeleteTask task, CancellationToken cancellationToken = default);
    }
}

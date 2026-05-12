using DevTask.Core.Database.Interfaces;
using DevTask.Core.Models.Dto;
using DevTask.Core.Models.Entity;
using DevTask.Core.Models.Errors;
using DevTask.Core.Models.Events;
using DevTask.Core.Services.Interfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevTask.Core.Services
{
    //TODO add all logic to mehods
    internal sealed class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, IRabbitMqService rabbitMqService, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        public async Task<Result<Guid>> AddAsync(CreateTask task, CancellationToken cancellationToken = default)
        {
            var taskItem = new TaskItem
            {
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                Priority = task.Priority,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _taskRepository.AddAsync(taskItem, cancellationToken);

            return Result.Ok(taskItem.Id);
        }

        public async Task<Result> DeleteAsync(DeleteTask task, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<TaskDto>>> GetAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        //TODO use outbox in transaction to prevent losing an events
        public async Task<Result> CompleteAsync(CompleteTask task, CancellationToken cancellationToken = default)
        {
            try
            {
                var taskItem = await _taskRepository.FirstOrDefaultAsync(task.Id, cancellationToken);

                if (taskItem is null)
                {
                    return Result.Fail(new NotFoundError());
                }

                taskItem.IsCompleted = true;
                taskItem.CompletedAt = DateTimeOffset.UtcNow;

                await _taskRepository.UpdateAsync(taskItem, cancellationToken);

                _ = PublishSilently(taskItem, cancellationToken);

                return Result.Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrent updating exception, entity date {TaskEntity}", task);

                return Result.Fail(new ConflictError());
            }
        }

        private async Task PublishSilently(TaskItem taskItem, CancellationToken cancellationToken = default)
        {
            try
            {
                var message = new UpdateTaskEvent(taskItem.Id, taskItem.Title, taskItem.CompletedAt!.Value, taskItem.Priority.ToString());

                await _rabbitMqService.PublishAsync(message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish event to RabbitMQ {Message}", taskItem);
            }
        }
    }
}

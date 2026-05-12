using DevTask.Api.Contracts.Task;
using DevTask.Api.Extensions;
using DevTask.Core;
using DevTask.Core.Models.Dto;
using DevTask.Core.Services.Interfaces;
using DevTask.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Results;

namespace DevTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddCustomValidators();

            builder.Services.AddService();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            //TODO clear and move to separate extension
            app.MapPost("/tasks", async ([FromServices] ITaskService service, [FromBody] CreateTaskRequest body, CancellationToken cancellationToken = default) =>
            {
                var task = CreateTask.ToDto(body.Title, body.IsCompleted, body.Priority);
                var result = await service.AddAsync(task, cancellationToken);

                return result.ToMinimalApiResult();
            })
            .AddEndpointFilter<ValidationFilter<CreateTaskRequest>>()
            .WithName("createTask");

            app.MapGet("/tasks", async ([FromServices] ITaskService service, CancellationToken cancellationToken = default) =>
            {
                var tasks = await service.GetAsync(cancellationToken);
            })
            .WithName("getTasks");

            app.MapPut("/tasks/{id}/complete", async ([FromServices] ITaskService service, [AsParameters] UpdateTaskRequest request, CancellationToken cancellationToken = default) =>
            {
                var task = CompleteTask.ToDto(request.Id);
                var result = await service.CompleteAsync(task, cancellationToken);

                return result.ToMinimalApiResult();
            })
            .AddEndpointFilter<ValidationFilter<UpdateTaskRequest>>()
            .WithName("updateTask");

            app.MapDelete("/tasks/{id}", async ([FromServices] ITaskService service, [FromRoute] string id, CancellationToken cancellationToken = default) =>
            {
                var task = DeleteTask.ToDto(id);
                var tasks = await service.DeleteAsync(task, cancellationToken);
            })
           .WithName("deleteTasks");

            app.Run();
        }
    }
}

using DevTask.Core.Services;
using DevTask.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevTask.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}

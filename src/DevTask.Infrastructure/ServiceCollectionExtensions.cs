using DevTask.Core.Database.Interfaces;
using DevTask.Infrastructure.Persistence.DbContexts;
using DevTask.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DevTask.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddDbContext<AppDbContext>(
                    (serviceProvider, contextOptions) =>
                    {
                        contextOptions.UseNpgsql(configuration.GetConnectionString(AppDbContext.ConnectionStringName));
                    });


            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.TryAddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}

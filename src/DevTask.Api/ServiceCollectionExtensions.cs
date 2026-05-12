using System.Reflection;

namespace DevTask.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomValidators(this IServiceCollection services)
        {
            Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(a => a.Name.EndsWith("Validator") && !a.IsAbstract && !a.IsInterface)
                    .Select(a => new { assignedType = a })
                    .ToList()
                    .ForEach(typesToRegister =>
                    {
                        services.AddScoped(Type.GetType(typesToRegister.assignedType.BaseType!.AssemblyQualifiedName!)!, typesToRegister.assignedType);
                    });

            return services;
        }
    }
}

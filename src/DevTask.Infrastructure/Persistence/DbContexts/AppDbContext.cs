using DevTask.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace DevTask.Infrastructure.Persistence.DbContexts
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options)
    {
        public const string ConnectionStringName = "DefaultConnection";

        public DbSet<TaskItem> TaskItems => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

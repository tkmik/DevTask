using DevTask.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevTask.Infrastructure.Persistence.EntityConfiguration
{
    internal sealed class TaskEntityConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired(true);
            builder.Property(x => x.IsCompleted)
                .IsRequired(true);
            builder.Property(x => x.CreatedAt)
                .IsRequired(false);
            builder.Property(x => x.CompletedAt)
                .IsRequired(false);
            builder.Property(x => x.Priority)
                .HasConversion(new EnumToStringConverter<PriorityType>());

            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}

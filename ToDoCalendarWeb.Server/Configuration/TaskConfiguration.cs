using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ToDoCalendarWeb.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Task> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(e => e.Id)
            .HasValueGenerator<GuidValueGenerator>();

        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.PeriodId)
            .IsRequired();

        builder.HasIndex(t => t.PeriodId);
    }
}


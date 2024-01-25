using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<Domain.Task>
{
    public void Configure(EntityTypeBuilder<Domain.Task> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.PeriodId)
            .IsRequired();

        builder.HasOne<Period>()
            .WithMany()
            .HasForeignKey(t => t.PeriodId);

        builder.Ignore(t => t.IsTracked);

        builder.HasIndex(t => t.PeriodId);
    }
}


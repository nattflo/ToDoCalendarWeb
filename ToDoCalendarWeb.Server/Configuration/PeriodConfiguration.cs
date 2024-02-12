using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;
using Newtonsoft.Json;

namespace ToDoCalendarWeb.Configuration;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.RoutineId)
            .IsRequired();

        builder.Property(p => p.DayOfWeek)
            .IsRequired();

        builder.Property(p => p.TimePeriod)
            .HasConversion(
                tp => JsonConvert.SerializeObject(tp),
                tp => JsonConvert.DeserializeObject<TimePeriod>(tp));

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(p => p.IsTracked);
    }
}

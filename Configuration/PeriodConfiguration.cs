using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;
using System.Text.Json;
using NuGet.Protocol;
using Newtonsoft.Json;

namespace ToDoCalendarWeb.Configuration;

public class PeriodConfiguration : IEntityTypeConfiguration<Period>
{
    public void Configure(EntityTypeBuilder<Period> builder)
    {
        var jsonOptions = new JsonSerializerOptions();
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.RoutineId)
            .IsRequired();

        builder.HasOne<Routine>()
            .WithMany()
            .HasForeignKey(p => p.RoutineId);

        builder.Property(p => p.DayOfWeek)
            .IsRequired();

        builder.Property(p => p.TimePeriod)
            .HasConversion(
                tp => JsonConvert.SerializeObject(tp),
                tp => JsonConvert.DeserializeObject<TimePeriod>(tp));

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Routine>()
            .WithMany()
            .HasForeignKey(p => p.RoutineId);

        builder.Ignore(p => p.IsTracked);
    }
}

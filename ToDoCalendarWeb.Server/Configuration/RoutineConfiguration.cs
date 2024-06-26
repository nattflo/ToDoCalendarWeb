﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ToDoCalendarWeb.Configuration;

public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(e => e.Id)
            .HasValueGenerator<GuidValueGenerator>();

        builder.Property(r => r.Name)
            .IsRequired();

        builder.HasMany(r => r.Periods)
            .WithOne()
            .HasForeignKey(p => p.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(r => r.IsTracked);
    }
}

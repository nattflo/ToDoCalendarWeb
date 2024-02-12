using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;
using Newtonsoft.Json;

namespace ToDoCalendarWeb.Configuration;

public class DiffConfiguration : IEntityTypeConfiguration<Diff>
{
    public void Configure(EntityTypeBuilder<Diff> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ObjectId)
            .IsRequired();

        builder.Property(e => e.ObjectType)
            .IsRequired();

        builder.Property(e => e.ChangeTime)
            .IsRequired();

        builder.Property(e => e.PropName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.From)
            .IsRequired()
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<object>(v));

        builder.Property(e => e.To)
            .IsRequired()
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<object>(v));
    }
}

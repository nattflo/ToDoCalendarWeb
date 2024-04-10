using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoCalendarWeb.Configuration;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Period> Periods { get; set; }
    public virtual DbSet<Domain.Task> Tasks { get; set; }
    public virtual DbSet<Routine> Routines { get; set; }
    public virtual DbSet<Diff> Diffs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new RoutineConfiguration().Configure(modelBuilder.Entity<Routine>());
        new PeriodConfiguration().Configure(modelBuilder.Entity<Period>());
        new TaskConfiguration().Configure(modelBuilder.Entity<Domain.Task>());
        new DiffConfiguration().Configure(modelBuilder.Entity<Diff>());

        modelBuilder.Seed();

    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}

using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Configuration;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public class AppDbContext : DbContext
{
    public virtual DbSet<Period> Periods { get; set; }
    public virtual DbSet<Domain.Task> Tasks { get; set; }
    public virtual DbSet<Routine> Routines { get; set; }
    public virtual DbSet<Diff> Diffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=todoCalendarDb;User Id=postgres;Password=4801;");
        base.OnConfiguring(optionsBuilder);
    }

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

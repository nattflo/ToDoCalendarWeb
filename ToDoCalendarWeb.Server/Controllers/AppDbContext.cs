using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Configuration;
using ToDoCalendarWeb.Domain;
using ToDoCalendarWeb.Interceptors;

namespace ToDoCalendarWeb.Controllers;

public class AppDbContext : DbContext
{
    public DbSet<Period> Periods { get; set; }
    public DbSet<Domain.Task> Tasks { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Diff> Diffs { get; set; }

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

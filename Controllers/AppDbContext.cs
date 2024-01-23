using Microsoft.EntityFrameworkCore;
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
        optionsBuilder.UseInMemoryDatabase("test").AddInterceptors(new TrackChangesInterceptor(this));
        base.OnConfiguring(optionsBuilder);
    }
}

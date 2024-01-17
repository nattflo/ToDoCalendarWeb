using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public class AppDbContext : DbContext
{
    public DbSet<Period> Periods { get; set; }
    public DbSet<Domain.Task> Tasks { get; set; }
    public DbSet<Routine> Routines { get; set; }
}

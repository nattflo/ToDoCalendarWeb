using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToDoCalendarWeb.Domain;
using ToDoCalendarWeb.Controllers;

namespace ToDoCalendarWeb.Interceptors;

public class TrackChangesInterceptor(AppDbContext appDbContext) : SaveChangesInterceptor
{
    public readonly AppDbContext AppDbContext = appDbContext;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context != null)
        {
            AuditChanges(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private void AuditChanges(DbContext context)
    {
        var changed = context.ChangeTracker.Entries()
            .Where(e => e.Entity != null &&
                e.Entity is AbstractTrackableEntity entity &&
                entity.IsTracked &&
                e.State == EntityState.Modified).ToList();

        if (changed != null && changed.Any())
        {
            foreach (var entry in changed)
            {
                var entryType = entry.Entity.GetType();
                var id = ((AbstractTrackableEntity)entry.Entity).Id;

                if (entryType != null)
                {
                    var chand = entry.Properties
                        .Where(p => p.IsModified && !p.OriginalValue.Equals(p.CurrentValue)).ToList();
                    AppDbContext.AddRangeAsync(entry.Properties
                        .Where(p => p.IsModified)
                        .Select(p =>
                            new Diff()
                            {
                                ObjectType = entryType.Name,
                                ObjectId = id,
                                From = p.OriginalValue,
                                To = p.CurrentValue,
                                PropName = p.Metadata.Name,
                                ChangeTime = DateTime.UtcNow
                            }
                    ));
                    AppDbContext.SaveChanges();
                }
            }

        }
    }
}

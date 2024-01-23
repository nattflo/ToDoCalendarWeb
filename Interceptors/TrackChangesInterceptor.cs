using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToDoCalendarWeb.Domain;
using ToDoCalendarWeb.Controllers;

namespace ToDoCalendarWeb.Interceptors;

public class TrackChangesInterceptor(AppDbContext appDbContext) : SaveChangesInterceptor
{
    public readonly AppDbContext AppDbContext = appDbContext;

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        AuditChanges(eventData);
        return base.SavedChanges(eventData, result);
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        AuditChanges(eventData);
        return base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditChanges(SaveChangesCompletedEventData eventData)
    {
        var changed = eventData.Context?.ChangeTracker.Entries()
            .Where(e => e.Entity != null &&
                e.Entity is AbstractTrackableEntity entity &&
                entity.IsTracked &&
                e.State == EntityState.Modified);

        if (changed != null && changed.Any())
        {
            foreach (var entry in changed)
            {
                var entryType = entry.Entity.GetType().BaseType;
                var id = ((AbstractTrackableEntity)entry.Entity).Id;

                if (entryType != null)
                {
                    AppDbContext.AddRangeAsync(entry.Properties
                        .Where(p => p.IsModified)
                        .Select(p =>
                            new Diff()
                            {
                                ObjectType = entryType,
                                ObjectId = id,
                                From = p.OriginalValue,
                                To = p.CurrentValue,
                                PropName = p.Metadata.Name,
                                ChangeTime = DateTime.UtcNow
                            }
                    ));
                }
            }

        }
    }
}

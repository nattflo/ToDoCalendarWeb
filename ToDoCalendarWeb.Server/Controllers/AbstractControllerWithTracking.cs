using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public abstract class AbstractControllerWithTracking<TEntity>(AppDbContext context): CRUDController<TEntity, AppDbContext>(context) where TEntity : AbstractTrackableEntity
{
    [HttpGet("{entityId}/diffs/{id}")]
    public async Task<ActionResult<Diff>> GetDiff([FromRoute] Guid entityId, [FromRoute] Guid id)
    {
        var diff = await _context.Set<Diff>().FirstOrDefaultAsync(d => d.Id == id && d.ObjectId == entityId && d.ObjectType == typeof(TEntity).Name);

        if (diff == null)
        {
            return NotFound();
        }

        return diff;
    }

    [HttpGet("{entityId}/diffs")]
    public async Task<ActionResult<IEnumerable<Diff>>> GetDiffs([FromRoute] Guid entityId)
    {
        var diffs = await _context.Set<Diff>().Where(d => d.ObjectId == entityId && d.ObjectType == typeof(TEntity).ToString()).ToArrayAsync();

        if (diffs == null || diffs.Length == 0)
        {
            return NotFound();
        }

        return diffs;
    }

    [HttpPut("{id}")]
    public override async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] TEntity entity)
    {
        entity.IsTracked = true;
        var originalEntity = _context.Set<TEntity>().AsNoTracking().FirstOrDefault(e => e.Id == id);

        var result = await base.Put(id, entity);
        var props = entity.GetType().GetProperties();

        if (originalEntity != null)
        {
            var diffs = new List<Diff>();
            foreach (var prop in props)
            {
                var originalValue = prop.GetValue(originalEntity);
                var currentValue = prop.GetValue(entity);

                if (originalValue != null && currentValue != null && !originalValue.Equals(currentValue))
                {
                    diffs.Add(new()
                    {
                        ObjectId = id,
                        ObjectType = typeof(TEntity).ToString(),
                        PropName = prop.Name,
                        From = originalValue,
                        To = currentValue,
                        ChangeTime = DateTime.UtcNow
                    });
                }
            }
            _context.Set<Diff>().AddRange(diffs);
            await _context.SaveChangesAsync();
        }
        return result;
    }

    [HttpPut("{entityId}/diffs/{id}/rollback")]
    public async Task<ActionResult> Rollback([FromRoute] Guid entityId, [FromRoute] Guid id)
    {
        var diff = await _context.Set<Diff>().FirstOrDefaultAsync(d => d.Id == id && d.ObjectId == entityId && d.ObjectType == typeof(TEntity).Name);

        if (diff == null)
        {
            return BadRequest();
        }

        var entity =  await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entityId);

        if (entity == null)
        {
            return BadRequest();
        }
        _context.Entry(entity).Property(diff.PropName).CurrentValue = diff.From;
        entity.IsTracked = _context.Set<Diff>().Any(d => d.PropName == diff.PropName && d.ChangeTime >= diff.ChangeTime && d.Id != id); //Имеются ли изменения этого поля, созданные после

        await _context.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = entity.Id }, entity);
    }
}

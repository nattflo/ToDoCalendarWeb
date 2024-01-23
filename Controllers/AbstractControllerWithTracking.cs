using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public abstract class AbstractControllerWithTracking<TEntity>(AppDbContext context): CRUDController<TEntity, AppDbContext>(context) where TEntity : AbstractTrackableEntity
{
    [HttpGet("{entityId}/diffs/{id}")]
    public async Task<ActionResult<Diff>> GetDiff([FromRoute] Guid entityId, [FromRoute] Guid id)
    {
        var diff = await _context.Set<Diff>().FirstOrDefaultAsync(d => d.Id == id && d.ObjectId == entityId && d.ObjectType == typeof(TEntity));

        if (diff == null)
        {
            return NotFound();
        }

        return diff;
    }

    [HttpGet("{entityId}/diffs")]
    public async Task<ActionResult<IEnumerable<Diff>>> GetDiffs([FromRoute] Guid entityId)
    {
        var diffs = await _context.Set<Diff>().Where(d => d.ObjectId == entityId && d.ObjectType == typeof(TEntity)).ToArrayAsync();

        if (diffs == null || diffs.Length == 0)
        {
            return NotFound();
        }

        return diffs;
    }

    [HttpPut("{id}")]
    public override Task<IActionResult> Put([FromRoute] Guid id, [FromBody] TEntity entity)
    {
        entity.IsTracked = true;
        return base.Put(id, entity);
    }

    [HttpPut("{entityId}/diffs/{id}/rollback")]
    public async Task<ActionResult> Rollback([FromRoute] Guid entityId, [FromRoute] Guid id)
    {
        var diff = await _context.Set<Diff>().FirstOrDefaultAsync(d => d.Id == id && d.ObjectId == entityId && d.ObjectType == typeof(TEntity));

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

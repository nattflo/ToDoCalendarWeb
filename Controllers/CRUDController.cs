using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public abstract class CRUDController<TEntity, TDbContext>(TDbContext context) : ControllerBase where TEntity : AbstractEntity where TDbContext : DbContext
{
    protected readonly TDbContext _context = context;

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TEntity>> Get([FromRoute] Guid id)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

        if (entity == null)
        {
            return NotFound();
        }

        return entity;
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] TEntity entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        _context.Entry(entity).State = EntityState.Modified;
        var dbEntity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == id);

        if (dbEntity == null)
        {
            return NotFound();
        }

        dbEntity = entity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Post([FromBody] TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();

        return CreatedAtAction("Get", new { id = entity.Id }, entity);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
        {
            return NotFound();
        }

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

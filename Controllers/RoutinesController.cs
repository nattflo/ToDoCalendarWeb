using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

public class RoutinesController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Routine>>> GetRoutines()
    {
        return await _context.Routines.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Routine>> GetRoutine([FromRoute]Guid id)
    {
        var routine = await _context.Routines.FindAsync(id);

        if (routine == null)
        {
            return NotFound();
        }

        return routine;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask([FromRoute]Guid id, [FromBody]Domain.Task task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoutineExists(task.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Routine>> PostRoutine([FromBody]Routine routine)
    {
        _context.Routines.Add(routine);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetRoutine", new { id = routine.Id }, routine);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoutine([FromRoute]Guid id)
    {
        var routine = await _context.Routines.FindAsync(id);
        if (routine == null)
        {
            return NotFound();
        }

        _context.Routines.Remove(routine);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RoutineExists(Guid id)
    {
        return _context.Routines.Any(routine => routine.Id == id);
    }
}

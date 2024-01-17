using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Domain.Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Domain.Task>> GetTask([FromRoute]Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
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
            if (!TaskExists(task.Id))
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
    public async Task<ActionResult<Domain.Task>> PostTask([FromBody]Domain.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTask", new { id = task.Id }, task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask([FromRoute]Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TaskExists(Guid id)
    {
        return _context.Tasks.Any(task => task.Id == id);
    }
}

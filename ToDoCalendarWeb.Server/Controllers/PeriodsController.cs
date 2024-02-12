using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeriodsController(AppDbContext context) : AbstractControllerWithTracking<Period>(context)
{
    [HttpGet("{periodId}/tasks")]
    public async Task<ActionResult<IEnumerable<Domain.Task>>> GetTasks([FromRoute] Guid periodId)
    {
        var tasks = await _context.Tasks.Where(t => t.PeriodId == periodId).ToArrayAsync();

        if (tasks == null || tasks.Length == 0)
        {
            return NoContent();
        }

        return tasks;
    }
}

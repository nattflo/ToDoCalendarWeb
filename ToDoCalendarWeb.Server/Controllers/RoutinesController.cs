using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoutinesController(AppDbContext context) : AbstractControllerWithTracking<Routine>(context)
{
    [HttpGet("{routineId}/periods")]
    public async Task<ActionResult<IEnumerable<Period>>> GetPeriods([FromRoute] Guid routineId)
    {
        var periods = await _context.Set<Period>().Where(t => t.RoutineId == routineId).ToArrayAsync();

        if (periods == null || periods.Length == 0)
        {
            return NoContent();
        }

        return periods;
    }
}
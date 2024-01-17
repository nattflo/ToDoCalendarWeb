using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCalendarWeb.Domain;

namespace ToDoCalendarWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Period>>> GetPeriods()
        {
            return await _context.Periods.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Period>> GetPeriod([FromRoute]Guid id)
        {
            var period = await _context.Periods.FindAsync(id);

            if (period == null)
            {
                return NotFound();
            }

            return period;
        }

        [HttpPut]
        public async Task<IActionResult> PutPeriod([FromRoute] Guid id, [FromBody]Period period)
        {
            if (id != period.Id)
            {
                return BadRequest();
            }

            _context.Entry(period).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeriodExists(period.Id))
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
        public async Task<ActionResult<Period>> PostPeriod([FromBody]Period period)
        {
            _context.Periods.Add(period);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeriod", new { id = period.Id }, period);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeriod([FromRoute]Guid id)
        {
            var period = await _context.Periods.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }

            _context.Periods.Remove(period);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeriodExists(Guid id)
        {
            return _context.Periods.Any(period => period.Id == id);
        }
    }
}

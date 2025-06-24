using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobShortage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobShortage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShortageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Shortage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shortage>>> GetShortages()
        {
            return await _context.Shortages
                                 .Include(s => s.Applicant)
                                 .Include(s => s.JobDescription)
                                 .ToListAsync();
        }

        // GET: api/Shortage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shortage>> GetShortage(int id)
        {
            var shortage = await _context.Shortages
                                         .Include(s => s.Applicant)
                                         .Include(s => s.JobDescription)
                                         .FirstOrDefaultAsync(s => s.Id == id);

            if (shortage == null)
            {
                return NotFound();
            }

            return shortage;
        }

        // POST: api/Shortage
        [HttpPost]
        public async Task<ActionResult<Shortage>> PostShortage(Shortage shortage)
        {
            // It's good practice to validate ApplicantId and JobDescriptionId exist
            // For brevity, this is omitted here but should be considered for robust applications.
            _context.Shortages.Add(shortage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShortage), new { id = shortage.Id }, shortage);
        }

        // PUT: api/Shortage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShortage(int id, Shortage shortage)
        {
            if (id != shortage.Id)
            {
                return BadRequest();
            }

            _context.Entry(shortage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShortageExists(id))
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

        // DELETE: api/Shortage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortage(int id)
        {
            var shortage = await _context.Shortages.FindAsync(id);
            if (shortage == null)
            {
                return NotFound();
            }

            _context.Shortages.Remove(shortage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShortageExists(int id)
        {
            return _context.Shortages.Any(e => e.Id == id);
        }
    }
}

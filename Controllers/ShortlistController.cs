using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobShortage.Models; // Assuming Shortlist is in JobShortage.Models, adjust if necessary
using shortlist.Models; // Added for Shortlist if it's in shortlist.Models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobShortage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShortlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Shortlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shortlist>>> GetShortlists()
        {
            // Include related data if necessary, e.g., BUReviewers
            return await _context.Shortlists.Include(s => s.BUReviewers).ToListAsync();
        }

        // GET: api/Shortlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shortlist>> GetShortlist(int id)
        {
            // Include related data if necessary
            var shortlist = await _context.Shortlists.Include(s => s.BUReviewers)
                                                     .FirstOrDefaultAsync(s => s.Id == id);

            if (shortlist == null)
            {
                return NotFound();
            }

            return shortlist;
        }

        // POST: api/Shortlist
        [HttpPost]
        public async Task<ActionResult<Shortlist>> PostShortlist(Shortlist shortlist)
        {
            _context.Shortlists.Add(shortlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShortlist), new { id = shortlist.Id }, shortlist);
        }

        // PUT: api/Shortlist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShortlist(int id, Shortlist shortlist)
        {
            if (id != shortlist.Id)
            {
                return BadRequest();
            }

            _context.Entry(shortlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShortlistExists(id))
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

        // DELETE: api/Shortlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortlist(int id)
        {
            var shortlist = await _context.Shortlists.FindAsync(id);
            if (shortlist == null)
            {
                return NotFound();
            }

            _context.Shortlists.Remove(shortlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShortlistExists(int id)
        {
            return _context.Shortlists.Any(e => e.Id == id);
        }
    }
}

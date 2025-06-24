using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobShortage.Models; // Assuming BUReviewer is in JobShortage.Models, adjust if necessary
using shortlist.Models; // Added for BUReviewer if it's in shortlist.Models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobShortage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BUReviewerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BUReviewerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BUReviewer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BUReviewer>>> GetBUReviewers()
        {
            // Include related data if necessary, e.g., Shortlist
            return await _context.BUReviewers.Include(br => br.Shortlist).ToListAsync();
        }

        // GET: api/BUReviewer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BUReviewer>> GetBUReviewer(int id)
        {
            // Include related data if necessary
            var bUReviewer = await _context.BUReviewers.Include(br => br.Shortlist)
                                                       .FirstOrDefaultAsync(br => br.Id == id);

            if (bUReviewer == null)
            {
                return NotFound();
            }

            return bUReviewer;
        }

        // POST: api/BUReviewer
        [HttpPost]
        public async Task<ActionResult<BUReviewer>> PostBUReviewer(BUReviewer bUReviewer)
        {
            // Validate ShortlistId if it's a required field and not automatically handled
            // For example, check if _context.Shortlists.Any(s => s.Id == bUReviewer.ShortlistId)
            _context.BUReviewers.Add(bUReviewer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBUReviewer), new { id = bUReviewer.Id }, bUReviewer);
        }

        // PUT: api/BUReviewer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBUReviewer(int id, BUReviewer bUReviewer)
        {
            if (id != bUReviewer.Id)
            {
                return BadRequest();
            }

            _context.Entry(bUReviewer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BUReviewerExists(id))
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

        // DELETE: api/BUReviewer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBUReviewer(int id)
        {
            var bUReviewer = await _context.BUReviewers.FindAsync(id);
            if (bUReviewer == null)
            {
                return NotFound();
            }

            _context.BUReviewers.Remove(bUReviewer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BUReviewerExists(int id)
        {
            return _context.BUReviewers.Any(e => e.Id == id);
        }
    }
}

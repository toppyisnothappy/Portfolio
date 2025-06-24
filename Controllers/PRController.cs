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
    public class PRController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PRController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PR
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PR>>> GetPRs()
        {
            return await _context.PRs.Include(p => p.PRMappings).ToListAsync();
        }

        // GET: api/PR/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PR>> GetPR(int id)
        {
            var pr = await _context.PRs.Include(p => p.PRMappings)
                                       .FirstOrDefaultAsync(p => p.Id == id);

            if (pr == null)
            {
                return NotFound();
            }

            return pr;
        }

        // POST: api/PR
        [HttpPost]
        public async Task<ActionResult<PR>> PostPR(PR pr)
        {
            _context.PRs.Add(pr);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPR), new { id = pr.Id }, pr);
        }

        // PUT: api/PR/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPR(int id, PR pr)
        {
            if (id != pr.Id)
            {
                return BadRequest();
            }

            _context.Entry(pr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRExists(id))
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

        // DELETE: api/PR/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePR(int id)
        {
            var pr = await _context.PRs.FindAsync(id);
            if (pr == null)
            {
                return NotFound();
            }

            _context.PRs.Remove(pr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PRExists(int id)
        {
            return _context.PRs.Any(e => e.Id == id);
        }
    }
}

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
    public class PRMappingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PRMappingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PRMapping
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PRMapping>>> GetPRMappings()
        {
            return await _context.PRMappings.Include(prm => prm.Candidate)
                                           .Include(prm => prm.PR)
                                           .ToListAsync();
        }

        // GET: api/PRMapping/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PRMapping>> GetPRMapping(int id)
        {
            var pRMapping = await _context.PRMappings.Include(prm => prm.Candidate)
                                                     .Include(prm => prm.PR)
                                                     .FirstOrDefaultAsync(prm => prm.Id == id);

            if (pRMapping == null)
            {
                return NotFound();
            }

            return pRMapping;
        }

        // POST: api/PRMapping
        [HttpPost]
        public async Task<ActionResult<PRMapping>> PostPRMapping(PRMapping pRMapping)
        {
            _context.PRMappings.Add(pRMapping);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPRMapping), new { id = pRMapping.Id }, pRMapping);
        }

        // PUT: api/PRMapping/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPRMapping(int id, PRMapping pRMapping)
        {
            if (id != pRMapping.Id)
            {
                return BadRequest();
            }

            _context.Entry(pRMapping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PRMappingExists(id))
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

        // DELETE: api/PRMapping/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePRMapping(int id)
        {
            var pRMapping = await _context.PRMappings.FindAsync(id);
            if (pRMapping == null)
            {
                return NotFound();
            }

            _context.PRMappings.Remove(pRMapping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PRMappingExists(int id)
        {
            return _context.PRMappings.Any(e => e.Id == id);
        }
    }
}

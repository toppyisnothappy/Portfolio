using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobShortage.Models; // Assuming M2M_Shortlist_Shortage is in JobShortage.Models, adjust if necessary
using shortlist.Models; // Added for M2M_Shortlist_Shortage if it's in shortlist.Models
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobShortage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class M2M_Shortlist_ShortageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public M2M_Shortlist_ShortageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/M2M_Shortlist_Shortage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<M2M_Shortlist_Shortage>>> GetM2M_Shortlist_Shortages()
        {
            // Include related data if necessary, e.g., PRMapping
            return await _context.M2M_Shortlist_Shortages.Include(m => m.PRMapping).ToListAsync();
        }

        // GET: api/M2M_Shortlist_Shortage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<M2M_Shortlist_Shortage>> GetM2M_Shortlist_Shortage(int id)
        {
            // Include related data if necessary
            var m2m_Shortlist_Shortage = await _context.M2M_Shortlist_Shortages.Include(m => m.PRMapping)
                                                                              .FirstOrDefaultAsync(m => m.Id == id);

            if (m2m_Shortlist_Shortage == null)
            {
                return NotFound();
            }

            return m2m_Shortlist_Shortage;
        }

        // POST: api/M2M_Shortlist_Shortage
        [HttpPost]
        public async Task<ActionResult<M2M_Shortlist_Shortage>> PostM2M_Shortlist_Shortage(M2M_Shortlist_Shortage m2m_Shortlist_Shortage)
        {
            // Validate PRMappingId if it's a required field and not automatically handled
            _context.M2M_Shortlist_Shortages.Add(m2m_Shortlist_Shortage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetM2M_Shortlist_Shortage), new { id = m2m_Shortlist_Shortage.Id }, m2m_Shortlist_Shortage);
        }

        // PUT: api/M2M_Shortlist_Shortage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutM2M_Shortlist_Shortage(int id, M2M_Shortlist_Shortage m2m_Shortlist_Shortage)
        {
            if (id != m2m_Shortlist_Shortage.Id)
            {
                return BadRequest();
            }

            _context.Entry(m2m_Shortlist_Shortage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!M2M_Shortlist_ShortageExists(id))
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

        // DELETE: api/M2M_Shortlist_Shortage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteM2M_Shortlist_Shortage(int id)
        {
            var m2m_Shortlist_Shortage = await _context.M2M_Shortlist_Shortages.FindAsync(id);
            if (m2m_Shortlist_Shortage == null)
            {
                return NotFound();
            }

            _context.M2M_Shortlist_Shortages.Remove(m2m_Shortlist_Shortage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool M2M_Shortlist_ShortageExists(int id)
        {
            return _context.M2M_Shortlist_Shortages.Any(e => e.Id == id);
        }
    }
}

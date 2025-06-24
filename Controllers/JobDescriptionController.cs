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
    public class JobDescriptionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobDescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobDescription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDescription>>> GetJobDescriptions()
        {
            return await _context.JobDescriptions.Include(jd => jd.Shortages).ToListAsync();
        }

        // GET: api/JobDescription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDescription>> GetJobDescription(int id)
        {
            var jobDescription = await _context.JobDescriptions.Include(jd => jd.Shortages).FirstOrDefaultAsync(jd => jd.Id == id);

            if (jobDescription == null)
            {
                return NotFound();
            }

            return jobDescription;
        }

        // POST: api/JobDescription
        [HttpPost]
        public async Task<ActionResult<JobDescription>> PostJobDescription(JobDescription jobDescription)
        {
            _context.JobDescriptions.Add(jobDescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobDescription), new { id = jobDescription.Id }, jobDescription);
        }

        // PUT: api/JobDescription/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobDescription(int id, JobDescription jobDescription)
        {
            if (id != jobDescription.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobDescription).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobDescriptionExists(id))
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

        // DELETE: api/JobDescription/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobDescription(int id)
        {
            var jobDescription = await _context.JobDescriptions.FindAsync(id);
            if (jobDescription == null)
            {
                return NotFound();
            }

            _context.JobDescriptions.Remove(jobDescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobDescriptionExists(int id)
        {
            return _context.JobDescriptions.Any(e => e.Id == id);
        }
    }
}

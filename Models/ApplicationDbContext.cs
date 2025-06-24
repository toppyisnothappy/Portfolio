using Microsoft.EntityFrameworkCore;
// using shortlist.Models; // Will use JobShortage.Models for all
namespace JobShortage.Models // Assuming this namespace is correct for the existing models
using shortlist.Models; // Added for the new models
namespace JobShortage.Models // Assuming this is the correct namespace for ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<JobDescription> JobDescriptions { get; set; }
        public DbSet<Shortage> Shortages { get; set; }
        public DbSet<BUReviewer> BUReviewers { get; set; } // Added
        public DbSet<PRMapping> PRMappings { get; set; } // Added
        public DbSet<Shortlist> Shortlists { get; set; } // Added
        public DbSet<M2M_Shortlist_Shortage> M2M_Shortlist_Shortages { get; set; } // Added


        // New DbSets for Candidate, PR, and PRMapping
        public DbSet<Candidate> Candidates { get; set; } = null!;
        public DbSet<PR> PRs { get; set; } = null!;
        public DbSet<PRMapping> PRMappings { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Applicant -> Shortage (One-to-Many)
            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Shortages)
                .WithOne(s => s.Applicant)
                .HasForeignKey(s => s.ApplicantId);

            // Configure JobDescription -> Shortage (One-to-Many)
            modelBuilder.Entity<JobDescription>()
                .HasMany(jd => jd.Shortages)
                .WithOne(s => s.JobDescription)
                .HasForeignKey(s => s.JobDescriptionId);

            // Configure Candidate -> PRMapping (One-to-Many)
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.PRMappings)
                .WithOne(prm => prm.Candidate)
                .HasForeignKey(prm => prm.CandidateId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior

            // Configure PR -> PRMapping (One-to-Many)
            modelBuilder.Entity<PR>()
                .HasMany(p => p.PRMappings)
                .WithOne(prm => prm.PR)
                .HasForeignKey(prm => prm.PRId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior
            // Configure Shortlist -> BUReviewer (One-to-Many)
            modelBuilder.Entity<Shortlist>()
                .HasMany(s => s.BUReviewers)
                .WithOne(br => br.Shortlist)
                .HasForeignKey(br => br.ShortlistId);

            // Configure PRMapping -> M2M_Shortlist_Shortage (One-to-Many)
            modelBuilder.Entity<PRMapping>()
                .HasMany<M2M_Shortlist_Shortage>() // Assuming M2M_Shortlist_Shortage has a collection navigation property if PRMapping can have many M2M_Shortlist_Shortage
                .WithOne(m => m.PRMapping)
                .HasForeignKey(m => m.PRMappingId);

            // If M2M_Shortlist_Shortage is a pure join table for a many-to-many between Shortlist and Shortage,
            // and PRMapping has a one-to-many with M2M_Shortlist_Shortage, the above is correct.
            // If PRMapping has a direct relationship with M2M_Shortlist_Shortage entries, this configuration is suitable.
        }
    }
}

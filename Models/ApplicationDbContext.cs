using Microsoft.EntityFrameworkCore;
// using shortlist.Models; // Will use JobShortage.Models for all

namespace JobShortage.Models // Assuming this namespace is correct for the existing models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<JobDescription> JobDescriptions { get; set; }
        public DbSet<Shortage> Shortages { get; set; }

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
        }
    }
}

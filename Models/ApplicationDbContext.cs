using Microsoft.EntityFrameworkCore;

namespace JobShortage.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<JobDescription> JobDescriptions { get; set; }
        public DbSet<Shortage> Shortages { get; set; }

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
        }
    }
}

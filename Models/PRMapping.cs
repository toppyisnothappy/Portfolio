using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobShortage.Models // Changed namespace
{
    public class PRMapping
    {
        [Key]
        public int Id { get; set; }

        public int CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; } = null!;

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PR PR { get; set; } = null!;

        // Other mapping specific properties can be added here
    }
}

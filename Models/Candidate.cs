using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobShortage.Models // Changed namespace
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        // Other candidate properties can be added here

        public ICollection<PRMapping> PRMappings { get; set; } = new List<PRMapping>();
    }
}

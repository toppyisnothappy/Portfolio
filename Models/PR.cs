using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobShortage.Models // Changed namespace
{
    public class PR
    {
        [Key]
        public int Id { get; set; }

        // Other PR properties can be added here

        public ICollection<PRMapping> PRMappings { get; set; } = new List<PRMapping>();
    }
}

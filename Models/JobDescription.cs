using System.Collections.Generic;

namespace JobShortage.Models
{
    public class JobDescription
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Shortage> Shortages { get; set; }

        public JobDescription()
        {
            Shortages = new HashSet<Shortage>();
        }
    }
}

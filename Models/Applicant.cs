using System.Collections.Generic;

namespace JobShortage.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Shortage> Shortages { get; set; }

        public Applicant()
        {
            Shortages = new HashSet<Shortage>();
        }
    }
}

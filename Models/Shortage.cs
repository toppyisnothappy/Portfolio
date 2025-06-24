namespace JobShortage.Models
{
    public class Shortage
    {
        public int Id { get; set; }
        public string Reason { get; set; }

        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }

        public int JobDescriptionId { get; set; }
        public virtual JobDescription JobDescription { get; set; }
    }
}

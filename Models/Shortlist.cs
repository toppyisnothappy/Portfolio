namespace shortlist.Models
{
    public class Shortlist
    {
        public int Id { get; set; }
        public string Name { get; set; } // Example property

        // Navigation property for BUReviewers
        public ICollection<BUReviewer> BUReviewers { get; set; }
    }
}

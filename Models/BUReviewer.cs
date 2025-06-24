namespace shortlist.Models
{
    public class BUReviewer
    {
        public int Id { get; set; }
        public string Name { get; set; } // Example property

        // Foreign key for Shortlist
        public int ShortlistId { get; set; }
        public Shortlist Shortlist { get; set; }
    }
}

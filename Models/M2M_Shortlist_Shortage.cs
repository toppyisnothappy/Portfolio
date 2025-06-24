namespace shortlist.Models
{
    public class M2M_Shortlist_Shortage
    {
        public int Id { get; set; }

        // Foreign key for PRMapping
        public int PRMappingId { get; set; }
        public PRMapping PRMapping { get; set; }

        // Other properties related to Shortlist and Shortage will be added later
    }
}

namespace EventsDisplay.Models
{
    public class Item
    {
        public uint id { get; set; }
        public string name { get; set; }

        public string description { get; set; }
        public string startDate { get; set; }

        public int venueId { get; set; }

        public Venue Venue { get; set; }
    }
}

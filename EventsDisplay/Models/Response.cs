namespace EventsDisplay.Models
{
    public class Response
    {

        public ICollection<Item> events { get; set; }

        public ICollection<Venue> venues { get; set; }

        public Venue venueDtl { get; set; }
    }
}

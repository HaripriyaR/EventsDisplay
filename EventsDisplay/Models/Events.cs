using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventsDisplay.Models
{
    public class Events
    {

        public ICollection<Item> EventsList { get; set; }

        public ICollection<Venue> VenueList { get; set;}

        public Venue venueDtl { get; set; }

        public SelectListItem selectedVenue { get; set; }
    }
}

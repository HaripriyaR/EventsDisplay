using Microsoft.EntityFrameworkCore;
using EventsDisplay.Models;
namespace EventsDisplay.Data
{
    public class EventContext:DbContext
    {   public EventContext(DbContextOptions options) :base(options) { }                                                                    
        public DbSet<Item> items { get; set; }

        public DbSet<Response> respList { get; set; }

    }
}

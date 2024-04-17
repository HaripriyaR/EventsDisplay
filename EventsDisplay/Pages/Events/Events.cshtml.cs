using EventsDisplay.Data;
using EventsDisplay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EventsDisplay.Pages.Events
{
    public class EventsModel1 : PageModel
    {
       



        private readonly EventContext _context;

        public EventsModel1(EventContext context)
        {
            _context = context;
        }




        public async void OnGet()
        {
            IQueryable<Item> eventsList = from s in _context.items
                                          select s;

            eventsList = (IQueryable<Item>)await eventsList.OrderByDescending(i => i.id).ToListAsync();
        }

       










    }
}

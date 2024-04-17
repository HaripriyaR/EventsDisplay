using EventsDisplay.Data;
using EventsDisplay.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EventsDisplay.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventContext _context;

        private readonly IMemoryCache _cache;

 
        //To use cache
        public IndexModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        public PaginatedList<Item> itemList { get; set; }

        public Response rsp { get; set; }

        public static Response rspcache { get; set; }

        public ICollection<Item> eventslist ;

        public string selectedVenue;

        public Venue venueDtl { get; set; }

        public SelectList venueList { get; set; }


        public async Task OnGetAsync(string selectedVenue)
        {

            Response resp = new Response();

            using (var httpClient = new HttpClient())
            {
                using(HttpResponseMessage response = await httpClient.GetAsync("https://teg-coding-challenge.s3.ap-southeast-2.amazonaws.com/events/event-data.json"))
                {
                    string apiResponse =  await response.Content.ReadAsStringAsync();   
                    resp = JsonConvert.DeserializeObject<Response>(apiResponse);
                }
            }
            //storing in venue list to opulate in dropdown

            venueList = new SelectList(resp.venues, nameof(Venue.Id), nameof(Venue.Name));  
         

            //loaded response to model

            rsp = resp;
            rsp.venueDtl = resp.venues.First<Venue>();

            venueDtl= resp.venues.First<Venue>();

            // storing the response in cache
            _cache.Set<Response>("eventsResp", rsp);

        }




        public void OnPost()
        {
            var id = Request.Form["selectedVenue"];

            

             Response responsestre = new Response();

            //extracting the response from cache

            responsestre = _cache.Get<Response>("eventsResp");

            rspcache = new Response();
            rspcache.events = responsestre.events;
            rspcache.venues = responsestre.venues;

            int selectedID = Convert.ToInt32(id);
            ICollection<Item> eventslist = rspcache.events.Where(x => x.venueId == selectedID).OrderBy(x=>x.startDate).ToList();

            ICollection<Venue> venuedtl = rspcache.venues.Where(x => x.Id == selectedID).ToList();

            //loading the venue list 
            venueList = new SelectList(rspcache.venues, nameof(Venue.Id), nameof(Venue.Name));
            rsp = new Response();

            rsp.events = eventslist;
            rsp.venues = rspcache.venues;
            //Display Venue Detail
            rsp.venueDtl = venuedtl.First<Venue>();
            venueDtl = venuedtl.First<Venue>();

           

        }










    }
}

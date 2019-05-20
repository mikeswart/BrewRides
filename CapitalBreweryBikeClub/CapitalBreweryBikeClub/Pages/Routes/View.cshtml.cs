using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class ViewModel : PageModel
    {
        public string EmbedLink { get; set; }
        public RouteInfo Route { get; set; }

        private readonly RouteProvider routeProvider;


        public ViewModel(RouteProvider routeProvider)
        {
            this.routeProvider = routeProvider;
        }

        public IActionResult OnGet(string routeName)
        {
            Route = routeProvider.Get(routeName);
            if (Route == null)
            {
                return NotFound();
            }

            EmbedLink = $"https://ridewithgps.com/routes/{Route.RideWithGpsId}/embed";
            return Page();
        }
    }
}
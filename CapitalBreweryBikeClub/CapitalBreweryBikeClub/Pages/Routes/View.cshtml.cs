using System;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class ViewModel : PageModel
    {
        public DailyRouteSchedule Route { get; private set; }

        private readonly RouteProvider routeProvider;

        public ViewModel(RouteProvider routeProvider)
        {
            this.routeProvider = routeProvider;
        }

        public IActionResult OnGet(string routeName)
        {
            var route = routeProvider.Get(routeName);
            if (route == null)
            {
                return NotFound();
            }

            Route = new DailyRouteSchedule(DateTime.MinValue, route);
            return Page();
        }
    }
}
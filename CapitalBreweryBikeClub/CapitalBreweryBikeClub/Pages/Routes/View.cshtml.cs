using System;
using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class ViewModel : PageModel
    {
        public DailyRouteSchedule Route { get; private set; }

        private readonly IServiceScopeFactory scopeFactory;

        public ViewModel(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public IActionResult OnGet(string routeName)
        {
            using var _ = scopeFactory.CreateDatabaseContextScope(out BrewRideDatabaseContext databaseContext);
            var route = databaseContext.Routes.FirstOrDefault(info => RouteInfo.GetWebFriendlyName(info.Name).Equals(routeName, StringComparison.InvariantCultureIgnoreCase));

            if (route == null)
            {
                return NotFound();
            }

            Route = new DailyRouteSchedule(DateTime.MinValue, route);
            return Page();
        }
    }
}

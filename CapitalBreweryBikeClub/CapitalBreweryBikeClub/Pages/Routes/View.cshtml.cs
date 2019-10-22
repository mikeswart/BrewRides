using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class ViewModel : PageModel
    {
        public RouteData Route { get; private set; }

        private readonly RouteDatabaseContext databaseContext;

        public ViewModel(RouteDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<IActionResult> OnGetAsync(string routeName)
        {
            Route = await FindRouteAsync(routeName);

            if (Route == null)
            {
               return NotFound();
            }

            return Page();
        }

        private async Task<RouteData> FindRouteAsync(string routeName)
        {
            // TODO: In the future it would be advantagous to keep a cache of routes that we know about and get the ID directly
            // when the caches misses, then we need to do the full search (like this)

            var routes = await databaseContext.Routes.ToListAsync();
            return routes.FirstOrDefault(route => RouteInfo.GetWebFriendlyName(route.Name).Equals(routeName, System.StringComparison.InvariantCultureIgnoreCase));
        }
    }
}

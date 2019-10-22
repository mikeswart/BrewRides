using System.Collections.Generic;
using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<(RouteData info, string link)> Routes
        {
            get;
            private set;
        }

        private readonly RouteDatabaseContext databaseContext;

        public IndexModel(RouteDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async void OnGetAsync()
        {
            var routes =  await databaseContext.Routes.ToListAsync();

            Routes = routes.Select(routeData => (routeData, RouteInfo.GetWebFriendlyName(routeData.Name)));
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class IndexModel : PageModel
    {
        public IEnumerable<(RouteInfo info, string link)> Routes
        {
            get;
            private set;
        }

        private readonly RouteProvider routeProvider;

        public IndexModel(RouteProvider routeProvider)
        {
            this.routeProvider = routeProvider;
        }

        public void OnGet()
        {
            Routes = routeProvider.Routes.Values.Select(info => (info, RouteInfo.GetWebFriendlyName(info.Name)));
        }
    }
}

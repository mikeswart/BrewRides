using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Routes
{
    public class IndexModel : PageModel
    {
        public IEnumerable<(RouteInfo info, string link)> Routes { get; }

        public IndexModel(RouteProvider routeProvider)
        {
            Routes = routeProvider.Routes.Select(info => (info, RouteInfo.GetWebFriendlyName(info.Name)));
        }

        public void OnGet()
        {
        }
    }
}
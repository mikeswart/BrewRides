using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages
{
    public class routeModel : PageModel
    {
        public string Route { get; set; }

        public void OnGet(string route)
        {
            if (route.Equals("list", StringComparison.InvariantCultureIgnoreCase))
            {
                // render list
            }

            Route = route;
        }
    }
}
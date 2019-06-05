using System.Collections.Generic;
using System.Linq;
using CapitalBreweryBikeClub.Data;
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

        private readonly BrewRideDatabaseContext dbContext;

        public IndexModel(BrewRideDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet()
        {
            Routes = dbContext.Routes.ToList().Select(info => (info, RouteInfo.GetWebFriendlyName(info.Name)));
        }
    }
}

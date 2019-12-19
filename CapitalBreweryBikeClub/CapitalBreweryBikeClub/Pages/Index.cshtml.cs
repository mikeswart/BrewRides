using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public SiteState SiteState
        {
            get;
            private set;
        }

        private readonly RouteDatabaseContext databaseContext;

        public IndexModel(RouteDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void OnGet()
        {
            SiteState = databaseContext.SiteState.First();
        }
    }
}
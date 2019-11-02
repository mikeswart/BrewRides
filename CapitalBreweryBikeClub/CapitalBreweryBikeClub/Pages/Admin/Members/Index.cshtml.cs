using System.Collections.Generic;
using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        public IEnumerable<MemberInformation> Members
        {
            get;
            private set;
        }

        private RouteDatabaseContext dbContext;

        public IndexModel(RouteDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void OnGet(RouteDatabaseContext dbContext)
        {
            Members = dbContext.Members.ToList();
        }
    }
}

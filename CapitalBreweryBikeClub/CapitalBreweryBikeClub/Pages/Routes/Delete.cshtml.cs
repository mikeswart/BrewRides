using System.Linq;
using CapitalBreweryBikeClub.Data;
using CapitalBreweryBikeClub.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize()]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public bool Confirm
        {
            get;
            set;
        }

        private readonly RouteDatabaseContext routeDatabaseContext;

        public DeleteModel(RouteDatabaseContext routeDatabaseContext)
        {
            this.routeDatabaseContext = routeDatabaseContext;
        }

        public IActionResult OnGet()
        {
            if(!User.IsInRole(Roles.Admin))
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostCancel()
        {
            var routeName = RouteData.Values["routeName"];
            return Redirect($"/Routes/{routeName}");
        }

        public IActionResult OnPostConfirm()
        {
            var routeName = RouteData.Values["routeName"] as string;

            var route = routeDatabaseContext.Routes.FirstOrDefault(route => string.Compare(route.Name, routeName) == 0);

            if(route == null)
            {
                return Redirect("/Routes");
            }

            routeDatabaseContext.Routes.Remove(route);
            routeDatabaseContext.SaveChanges();

            return Redirect("/Routes");
        }
    }
}

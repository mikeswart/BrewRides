using CapitalBreweryBikeClub.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    [Authorize()]
    public class DeleteModel : PageModel
    {
        public IActionResult OnGet()
        {
            if(!User.IsInRole(Roles.Admin))
            {
                return NotFound();
            }

            return Page();
        }
    }
}

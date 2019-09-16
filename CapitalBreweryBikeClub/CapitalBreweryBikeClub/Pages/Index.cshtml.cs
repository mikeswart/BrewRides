using CapitalBreweryBikeClub.Internal;
using CapitalBreweryBikeClub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostLogout()
        {
            return RedirectToPage("/Account/Signout");
        }
    }
}
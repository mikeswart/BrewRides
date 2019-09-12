using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Account.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public string UserName
        {
            get;
            private set;
        }

        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);

            UserName = user.UserName;

            if(User.Identity.IsAuthenticated)
            {
                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var user = await userManager.GetUserAsync(User);
            await userManager.DeleteAsync(user);
            return RedirectToPage("/");
        }
    }
}

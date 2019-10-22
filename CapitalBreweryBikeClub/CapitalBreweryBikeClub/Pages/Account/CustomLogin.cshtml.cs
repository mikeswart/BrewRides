using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CapitalBreweryBikeClub.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class CustomLoginModel : PageModel
    {
        private readonly RouteDatabaseContext dbContext;

        [BindProperty]
        public InputModel Input
        {
            get; set;
        }

        public CustomLoginModel(RouteDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult OnPostSignout()
        {
            return RedirectToPage("/Account/Signout");
        }

        public async Task<IActionResult> OnPostConfirmationAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            if(!dbContext.Members.Any(member => member.Email == Input.Email))
            {
                // Failed login, email not found.
                // TODO: Post state and reload
                return RedirectToPage();
            }

            var identity = new ClaimsIdentity(
                new[] { new Claim(ClaimTypes.Name, Input.Email) },
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                props);

            return RedirectToPage();
        }

        public void OnGet()
        {
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email
            {
                get; set;
            }
        }
    }
}

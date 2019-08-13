using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class CustomLoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input
        {
            get; set;
        }

        public async Task<IActionResult> OnPostSignoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage();
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

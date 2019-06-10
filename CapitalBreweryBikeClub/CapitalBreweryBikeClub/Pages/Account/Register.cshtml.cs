using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public string ReturnUrl
        {
            get;
            set;
        }

        public AuthenticationScheme GoogleLogin
        {
            get;
            set;
        }

        [BindProperty]
        public InputModel Input
        {
            get; set;
        }

        private readonly SignInManager<IdentityUser> signInManager;

        public RegisterModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            GoogleLogin = (await signInManager.GetExternalAuthenticationSchemesAsync()).First();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                    return LocalRedirect("/Index");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new
                    {
                        ReturnUrl = returnUrl,
                        RememberMe = Input.RememberMe
                    });
                }
                if (result.IsLockedOut)
                {
                    //_logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email
            {
                get; set;
            }

            [Required]
            public string Password
            {
                get; set;
            }

            [Display(Name = "Remember me?")]
            public bool RememberMe
            {
                get; set;
            }
        }
    }
}

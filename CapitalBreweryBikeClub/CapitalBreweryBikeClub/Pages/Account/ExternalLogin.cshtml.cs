using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapitalBreweryBikeClub.Pages.Account
{
    [Authorize()]
    public class ExternalLoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input
        {
            get; set;
        }

        public string LoginProvider
        {
            get; set;
        }

        public string ReturnUrl
        {
            get; set;
        }

        [TempData]
        public string ErrorMessage
        {
            get; set;
        }

        private readonly SignInManager<IdentityUser> signInManager;

        public ExternalLoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Register");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new
            {
                returnUrl
            });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login");
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToPage("./Login");
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                // Store the access token and resign in so the token is included in the cookie
                var user = await signInManager.UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var props = new AuthenticationProperties();
                props.StoreTokens(info.AuthenticationTokens);
                await signInManager.SignInAsync(user, props, info.LoginProvider);

                //_logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl ?? "/");
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }

                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                var user = new IdentityUser(email);
                var createResult = await signInManager.UserManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    createResult = await signInManager.UserManager.AddLoginAsync(user, info);
                    if(createResult.Succeeded)
                    {
                        var props = new AuthenticationProperties();
                        props.StoreTokens(info.AuthenticationTokens);

                        await signInManager.SignInAsync(user, props, info.LoginProvider);

                        return LocalRedirect("/");
                    }
                }


                return Page();
            }
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

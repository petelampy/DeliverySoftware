using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeliverySoftware.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index";

        private readonly SignInManager<DeliveryUser> __SignInManager;

        public LoginModel (SignInManager<DeliveryUser> signInManager, UserManager<DeliveryUser> userManager)
        {
            __SignInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLoginAsync (string username, string password, bool rememberMe, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult _IdentityResult = await __SignInManager
                    .PasswordSignInAsync(username, password, rememberMe, false);

                if (_IdentityResult.Succeeded)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage(INDEX_PAGE_PATH);
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Incorrect Username or Password!");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync ()
        {
            __SignInManager.SignOutAsync();

            return RedirectToPage(INDEX_PAGE_PATH);
        }
    }
}

using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeliverySoftware.Pages.Account
{
    public class LoginModel : PageModel
    {
        private const string INDEX_PAGE_PATH = "../Index";
        private const string ORDER_TRACKING_PAGE_PATH = "../Customer/OrderTracking";

        private readonly SignInManager<DeliveryUser> __SignInManager;
        private readonly IPackageController __PackageController;

        public LoginModel (SignInManager<DeliveryUser> signInManager, UserManager<DeliveryUser> userManager)
        {
            __SignInManager = signInManager;
            __PackageController = new PackageController();
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

        public async Task<IActionResult> OnPostLoginWithCodeAsync (string trackingCode)
        {
            bool _IsValidTrackingCode = __PackageController.DoesTrackingCodeExist(trackingCode);

            if(_IsValidTrackingCode)
            {
                return RedirectToPage(ORDER_TRACKING_PAGE_PATH, new { TrackingCode = trackingCode });
            }
            else
            {
                ModelState.AddModelError("trackingcode", "Invalid tracking code!");
                return Page();
            }
        }
    }
}

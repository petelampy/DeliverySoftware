using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private static readonly string DELIVERY_RUNS_PAGE_PATH = "./Driver/DeliveryRuns";
        private static readonly string PACKAGES_PAGE_PATH = "./Delivery/PackageManagement";
        private readonly ILogger<IndexModel> __Logger;
        private readonly IUserController __UserController;

        public IndexModel (ILogger<IndexModel> logger)
        {
            __Logger = logger;
            __UserController = new UserController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Driver)
            {
                return RedirectToPage(DELIVERY_RUNS_PAGE_PATH);
            }
            else if (_CurrentUser.UserType == UserType.Employee)
            {
                return RedirectToPage(PACKAGES_PAGE_PATH);
            }
            else
            {
                return Page();
            }
        }
    }
}
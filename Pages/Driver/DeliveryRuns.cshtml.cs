using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Driver
{
    [Authorize]
    public class DeliveryRunsModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IUserController __UserController;

        public DeliveryRunsModel ()
        {
            __UserController = new UserController();
            __DeliveryController = new DeliveryController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));


            if (_CurrentUser.UserType == UserType.Driver)
            {
                DeliveryRuns = __DeliveryController
                    .GetAllAvailable()
                    .Where(deliveryRun => deliveryRun.NumberOfPackages > 0)
                    .ToList();

                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public List<Business.Delivery.Delivery> DeliveryRuns { get; set; }
    }
}

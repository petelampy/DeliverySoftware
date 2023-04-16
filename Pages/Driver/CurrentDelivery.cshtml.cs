using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Driver
{
    public class CurrentDeliveryModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;

        public CurrentDeliveryModel ()
        {
            __UserController = new UserController();
            __DeliveryController = new DeliveryController();
            __PackageController = new PackageController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Driver)
            {
                if (DeliveryRunUID != null && DeliveryRunUID != Guid.Empty)
                {
                    DeliveryRun = __DeliveryController.Get(DeliveryRunUID);

                    CurrentDrop = __PackageController
                        .GetByDeliveryAndDropNumber(DeliveryRun.UID, DeliveryRun.CurrentDrop);

                    CurrentStopCustomer = __UserController.Get(CurrentDrop.CustomerUID);
                }
                else
                {
                    //INVALID TRACKING CODE ERROR
                }

                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public int GetTotalNumberOfDrops(Guid delivery_uid)
        {
            return __PackageController.GetPackageCountByDelivery(delivery_uid);
        }


        [BindProperty(SupportsGet = true)]
        public Package CurrentDrop { get; set; }

        [BindProperty(SupportsGet = true)]
        public DeliveryUser CurrentStopCustomer { get; set; }

        [BindProperty(SupportsGet = true)]
        public Business.Delivery.Delivery DeliveryRun { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid DeliveryRunUID { get; set; }
    }
}

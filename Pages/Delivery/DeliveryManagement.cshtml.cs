using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Delivery
{
    public class DeliveryManagementModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;

        public DeliveryManagementModel ()
        {
            __UserController = new UserController();
            __DeliveryController = new DeliveryController();
            __VanController = new VanController();
            __PackageController = new PackageController();
        }

        public bool DoesDeliveryHavePackages (Guid uid)
        {
            return __PackageController.GetPackageCountByDelivery(uid) > 0;
        }

        public string GetVanRegistration (Guid van_uid)
        {
            return __VanController.GetRegistration(van_uid);
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {

                Deliveries = __DeliveryController.GetAllAvailable();
                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeleteDelivery (Guid uid)
        {
            bool _DeliveryHasPackages = DoesDeliveryHavePackages(uid);

            if (_DeliveryHasPackages)
            {
                ModelState.AddModelError("", "Delivery in use, can't delete!");
                return Page();
            }
            else
            {
                __DeliveryController.Delete(uid);
                return RedirectToPage("DeliveryManagement");
            }
        }

        public List<Business.Delivery.Delivery> Deliveries { get; set; }
    }
}

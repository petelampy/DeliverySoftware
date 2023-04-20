using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Delivery
{
    public class PackageManagementModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;

        public PackageManagementModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
            __DeliveryController = new DeliveryController();
        }

        public string GetCustomerName (Guid customer_uid)
        {
            return __UserController.GetName(customer_uid);
        }

        public bool IsPackageOutForDelivery (Guid uid)
        {
            Package _Package = __PackageController.Get(uid);
            return _Package.IsAssignedToDelivery && __DeliveryController.HasDeliveryRunStarted(_Package.DeliveryUID);
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {

                Packages = __PackageController.GetAllUndelivered();
                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeletePackage (Guid uid)
        {
            if (IsPackageOutForDelivery(uid))
            {
                ModelState.AddModelError("", "Package out for delivery, can't delete!");
                return Page();
            }
            else
            {
                Package _Package = __PackageController.Get(uid);
                Guid _DeliveryUID = _Package.DeliveryUID;
                bool _PackageAssignedToDelivery = _Package.IsAssignedToDelivery;

                __PackageController.Delete(uid);

                if (_PackageAssignedToDelivery)
                {
                    int _NumberOfPackages = __PackageController.GetPackageCountByDelivery(_DeliveryUID);

                    Business.Delivery.Delivery _DeliveryRun = __DeliveryController.Get(_DeliveryUID);
                    _DeliveryRun.NumberOfPackages = _NumberOfPackages;
                    __DeliveryController.Update(_DeliveryRun);

                    List<Package> _PackagesRemaining = __PackageController.GetPackagesByDelivery(_DeliveryUID);

                    int _Counter = 1;
                    foreach (Package _PackageToUpdate in _PackagesRemaining)
                    {
                        _PackageToUpdate.DropNumber = _Counter;
                        __PackageController.Update(_PackageToUpdate);

                        _Counter += 1;
                    }
                }

                return RedirectToPage("PackageManagement");
            }
        }

        public List<Package> Packages { get; set; }
    }
}

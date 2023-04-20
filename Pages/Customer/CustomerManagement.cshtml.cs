using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Customer
{
    public class CustomerManagementModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;

        public CustomerManagementModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
        }

        public bool DoesCustomerHaveUndeliveredPackages (Guid uid)
        {
            return __PackageController.GetActivePackagesByCustomer(uid) > 0;
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));


            if (_CurrentUser.UserType == UserType.Employee)
            {
                Customers = __UserController.GetAllCustomers();
                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeleteCustomer (string id)
        {
            Guid _UID = new Guid(id);

            bool _CustomerHasPackages = DoesCustomerHaveUndeliveredPackages(_UID);

            if (_CustomerHasPackages)
            {
                ModelState.AddModelError("", "Customer in use, can't delete!");
                return Page();
            }
            else
            {
                __UserController.Delete(_UID);
                return RedirectToPage("CustomerManagement");
            }
        }

        public List<DeliveryUser> Customers { get; set; }
    }
}

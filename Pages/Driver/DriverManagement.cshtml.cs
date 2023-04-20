using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Driver
{
    public class DriverManagementModel : PageModel
    {

        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IUserController __UserController;
        private readonly IVanController __VanController;

        public DriverManagementModel ()
        {
            __UserController = new UserController();
            __VanController = new VanController();
        }

        public int GetDriverVanCount (Guid driver_uid)
        {
            return __VanController.GetCountByDriver(driver_uid);
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));


            if (_CurrentUser.UserType == UserType.Employee)
            {
                Drivers = __UserController.GetAllDrivers();
                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public async Task<IActionResult> OnGetDeleteDriver (string id)
        {
            Guid _UID = new Guid(id);

            bool _DriverHasVans = GetDriverVanCount(_UID) > 0;

            if (_DriverHasVans)
            {
                ModelState.AddModelError("", "Driver in use, can't delete!");
                return Page();
            }
            else
            {
                __UserController.Delete(_UID);
                return RedirectToPage("DriverManagement");
            }
        }

        public List<DeliveryUser> Drivers { get; set; }
    }
}

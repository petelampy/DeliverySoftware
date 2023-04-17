using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Fleet
{
    public class FleetManagementModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;
        private readonly IDeliveryController __DeliveryController;

        public FleetManagementModel ()
        {
            __UserController = new UserController();
            __VanController = new VanController();
            __DeliveryController = new DeliveryController();
        }
        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {

                Vans = __VanController.GetAll();
                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public string GetDriverName(Guid driver_uid)
        {
            return __UserController.GetName(driver_uid);
        }

        public int GetActiveDeliveries (Guid van_uid)
        {
            return __DeliveryController.GetCountByVan(van_uid);
        }

        public async Task<IActionResult> OnGetDeleteVan (Guid uid)
        {
            bool _VanHasDeliveries = GetActiveDeliveries(uid) > 0;

            if (_VanHasDeliveries)
            {
                ModelState.AddModelError("", "Van in use, can't delete!");
                return Page();
            }
            else
            {
                __VanController.Delete(uid);
                return RedirectToPage("FleetManagement");
            }
        }


        public List<Van> Vans { get; set; }
    }
}

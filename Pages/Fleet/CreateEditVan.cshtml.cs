using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Fleet
{
    public class CreateEditVanModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;
        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;

        public CreateEditVanModel ()
        {
            __UserController = new UserController();
            __VanController = new VanController();
            __DeliveryController = new DeliveryController();
            __PackageController = new PackageController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {
                if (UID != null && UID != Guid.Empty)
                {
                    Van = __VanController.Get(UID);

                }
                else
                {
                    Van = new Van();
                }

                CreateDriverSelector();

                return Page();

            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public IActionResult OnPost ()
        {
            ValidateModel();

            if (!ModelState.IsValid)
            {
                CreateDriverSelector();
                return Page();
            }

            if (UID != null && UID != Guid.Empty)
            {
                __VanController.Update(Van);
            }
            else
            {
                __VanController.Create(Van);
            }

            return RedirectToPage("FleetManagement");
        }

        private void CreateDriverSelector ()
        {
            List<DeliveryUser> _Drivers = __UserController.GetAllDrivers();

            DriverSelection = _Drivers.Select(driver =>
                new SelectListItem
                {
                    Text = driver.Forename + " " + driver.Surname,
                    Value = driver.Id,
                    Selected = Van.DriverUID.Equals(new Guid(driver.Id))
                }).ToList();
        }

        public void ValidateModel()
        {
            List<Business.Delivery.Delivery> _DeliveryRunsAssigned = __DeliveryController.GetByVan(Van.UID);
            List<int> _RunSizes = new List<int>();

            foreach(Business.Delivery.Delivery _DeliveryRun in _DeliveryRunsAssigned)
            {
                int _RunSize = __PackageController.GetPackagesByDelivery(_DeliveryRun.UID).Sum(package => package.Size);
                _RunSizes.Add(_RunSize);
            }

            if (_RunSizes.Any(runSize => runSize > Van.Capacity))
            {
                ModelState.AddModelError("Van.Capacity", "There are assigned runs taking up more than this capacity!");
            }

            if(Van.Capacity <= 0)
            {
                ModelState.AddModelError("Van.Capacity", "Invalid capacity value!");
            }
        }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
        [BindProperty(SupportsGet = true)]
        public Van Van { get; set; }
        public List<SelectListItem> DriverSelection { get; set; }
    }
}

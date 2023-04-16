using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Delivery
{
    public class CreateEditPackageModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";
        private readonly IUserController __UserController;
        private readonly IPackageController __PackageController;
        private readonly IDeliveryController __DeliveryController;
        private readonly IVanController __VanController;


        public CreateEditPackageModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
            __DeliveryController = new DeliveryController();
            __VanController = new VanController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {
                if (UID != null && UID != Guid.Empty)
                {
                    Package = __PackageController.Get(UID);
                }
                else
                {
                    Package = new Package();
                    Package.TrackingCode = GenerateNewTrackingCode();
                }

                CreateCustomerSelector();
                CreateDeliverySelector();

                return Page();

            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public string GenerateNewTrackingCode()
        {
            Random _Random = new Random();

            string _TrackingCode = "";

            for (int _Counter = 0; _Counter < 5; _Counter++)
            {
                int _RandomCharIntValue = _Random.Next(65, 91);
                char _RandomCharacter = Convert.ToChar(_RandomCharIntValue);
                _TrackingCode += _RandomCharacter;
            }

            int _RandomNumber = _Random.Next(10000, 90000);
            _RandomNumber += DateTime.Now.Millisecond;

            _TrackingCode += _RandomNumber;

            return _TrackingCode;
        }

        public IActionResult OnPost ()
        {
            if (UID != null && UID != Guid.Empty)
            {
                __PackageController.Update(Package);
            }
            else
            {
                __PackageController.Create(Package);
            }

            return RedirectToPage("PackageManagement");
        }

        private void CreateCustomerSelector ()
        {
            List<DeliveryUser> _Customers = __UserController.GetAllCustomers();

            CustomerSelection = _Customers.Select(customer =>
                new SelectListItem
                {
                    Text = customer.Forename + " " + customer.Surname,
                    Value = customer.Id,
                    Selected = Package.CustomerUID.Equals(new Guid(customer.Id))
                }).ToList();
        }

        private void CreateDeliverySelector ()
        {
            List<Business.Delivery.Delivery> _Deliveries = __DeliveryController.GetAllAvailable();

            DeliverySelection = _Deliveries.Select(delivery =>
                new SelectListItem
                {
                    Text = delivery.Id + " - " + __VanController.GetRegistration(delivery.VanUID),
                    Value = delivery.UID.ToString(),
                    Selected = Package.DeliveryUID.Equals(delivery.UID)
                }).ToList();
        }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
        [BindProperty(SupportsGet = true)]
        public Package Package { get; set; }
        public List<SelectListItem> CustomerSelection { get; set; }
        public List<SelectListItem> DeliverySelection { get; set; }
    }
}

using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using DeliverySoftware.Business.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Delivery
{
    public class CreateEditPackageModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IEmailController __EmailController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;

        public CreateEditPackageModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
            __DeliveryController = new DeliveryController();
            __VanController = new VanController();
            __EmailController = new EmailController();
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

        public string GenerateNewTrackingCode ()
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

        public IActionResult OnPost ()
        {
            ValidateModel();

            if (!ModelState.IsValid)
            {
                CreateCustomerSelector();
                CreateDeliverySelector();
                return Page();
            }

            if (UID != null && UID != Guid.Empty)
            {

                Guid _CurrentDeliveryUID = __PackageController.Get(Package.UID).DeliveryUID;

                __PackageController.Update(Package);

                if (_CurrentDeliveryUID != Package.DeliveryUID)
                {
                    UpdatePackageCount(_CurrentDeliveryUID);
                    UpdatePackageCount(Package.DeliveryUID);
                }
            }
            else
            {
                __PackageController.Create(Package);

                DeliveryUser _Customer = __UserController.Get(Package.CustomerUID);

                Email _CustomerNotification = new Email()
                {
                    Recipient = _Customer.Email,
                    Subject = "Your Package is in the system!",
                    Body = "Hi " + _Customer.Forename + ",\n\n"
                                + "Your order of " + Package.Description + " is now in the system!\n\n"
                                + "Your tracking code is: " + Package.TrackingCode + "\n\n"
                                + "Keep an eye out for an email when the package is out for delivery!\n\n"
                                + "Thanks, Delivery+ Software Solutions"
                };

                __EmailController.SendEmail(_CustomerNotification);

                UpdatePackageCount(Package.DeliveryUID);
            }

            return RedirectToPage("PackageManagement");
        }

        private void UpdatePackageCount (Guid delivery_uid)
        {
            int _NumberOfPackages = __PackageController.GetPackageCountByDelivery(delivery_uid);

            Business.Delivery.Delivery _DeliveryRun = __DeliveryController.Get(delivery_uid);

            _DeliveryRun.NumberOfPackages = _NumberOfPackages;

            __DeliveryController.Update(_DeliveryRun);
        }

        private void ValidateModel ()
        {
            Business.Delivery.Delivery _DeliveryRun = __DeliveryController.Get(Package.DeliveryUID);
            List<Package> _PackagesOnDelivery = __PackageController.GetPackagesByDelivery(Package.DeliveryUID);
            Van _SelectedVan = __VanController.Get(_DeliveryRun.VanUID);

            int _SizeUsed = _PackagesOnDelivery.Sum(package => package.Size);

            if (Package.UID == Guid.Empty)
            {
                if (Package.Size + _SizeUsed > _SelectedVan.Capacity)
                {
                    ModelState.AddModelError("Package.Size", "This package won't fit in the delivery van for this run!");
                }
            }
            else
            {
                Package _CurrentPackage = __PackageController.Get(Package.UID);

                if (Package.Size + (_SizeUsed - _CurrentPackage.Size) > _SelectedVan.Capacity)
                {
                    ModelState.AddModelError("Package.Size", "This package won't fit in the delivery van for this run!");
                }
            }

            if (Package.Size <= 0)
            {
                ModelState.AddModelError("Package.Size", "Invalid package size!");
            }
        }

        public List<SelectListItem> CustomerSelection { get; set; }
        public List<SelectListItem> DeliverySelection { get; set; }

        [BindProperty(SupportsGet = true)]
        public Package Package { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}

using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Customer
{
    public class OrderTrackingModel : PageModel
    {
        private readonly IUserController __UserController;
        private readonly IPackageController __PackageController;
        private readonly IDeliveryController __DeliveryController;

        public OrderTrackingModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
            __DeliveryController = new DeliveryController();
        }

        public IActionResult OnGet()
        {
            if (TrackingCode != null && TrackingCode.Length > 0)
            {
                Package = __PackageController.GetByTrackingCode(TrackingCode);
                Customer = __UserController.Get(Package.CustomerUID);
                DeliveryRun = __DeliveryController.Get(Package.DeliveryUID);

                Package _CurrentDelivery = __PackageController
                    .GetByDeliveryAndDropNumber(DeliveryRun.UID, DeliveryRun.CurrentDrop);

                DeliveryUser _CurrentStopCustomer = __UserController.Get(_CurrentDelivery.CustomerUID);
                DriverHouseNumber = _CurrentStopCustomer.HouseNumber.Value;
                DriverPostCode = _CurrentStopCustomer.PostCode;
            }
            else
            {
                //INVALID TRACKING CODE ERROR
            }

            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public string TrackingCode { get; set; }

        public Package Package { get; set; }

        public DeliveryUser Customer { get; set; }

        public Business.Delivery.Delivery DeliveryRun { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DriverHouseNumber { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DriverPostCode { get; set; }
    }
}

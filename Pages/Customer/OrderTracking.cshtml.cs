using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeliverySoftware.Pages.Customer
{
    public class OrderTrackingModel : PageModel
    {
        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;

        public OrderTrackingModel ()
        {
            __UserController = new UserController();
            __PackageController = new PackageController();
            __DeliveryController = new DeliveryController();
            __VanController = new VanController();
        }

        public IActionResult OnGet ()
        {
            if (TrackingCode != null && TrackingCode.Length > 0)
            {
                Package = __PackageController.GetByTrackingCode(TrackingCode);
                Customer = __UserController.Get(Package.CustomerUID);
                DeliveryRun = __DeliveryController.Get(Package.DeliveryUID);



                if (DeliveryRun.CurrentDrop == 1)
                {
                    Van _DeliveryVan = __VanController.Get(DeliveryRun.VanUID);

                    DriverPostCode = _DeliveryVan.DepotPostCode;
                }
                else
                {
                    Package _CurrentDelivery = __PackageController
                    .GetByDeliveryAndDropNumber(DeliveryRun.UID, DeliveryRun.CurrentDrop - 1);

                    DeliveryUser _PreviousStopCustomer = __UserController.Get(_CurrentDelivery.CustomerUID);

                    DriverHouseNumber = _PreviousStopCustomer.HouseNumber.Value;
                    DriverPostCode = _PreviousStopCustomer.PostCode;
                }

                IsOutForDelivery = DeliveryRun.Status == DeliveryStatus.Started;
            }

            return Page();
        }

        public DeliveryUser Customer { get; set; }
        public Business.Delivery.Delivery DeliveryRun { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DriverHouseNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DriverPostCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsOutForDelivery { get; set; }

        public Package Package { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TrackingCode { get; set; }
    }
}

using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Users;
using DeliverySoftware.Business.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Driver
{
    public class CurrentDeliveryModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IPackageController __PackageController;
        private readonly IUserController __UserController;
        private readonly IEmailController __EmailController;

        public CurrentDeliveryModel ()
        {
            __UserController = new UserController();
            __DeliveryController = new DeliveryController();
            __PackageController = new PackageController();
            __EmailController = new EmailController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Driver)
            {
                if (DeliveryRunUID != null && DeliveryRunUID != Guid.Empty)
                {
                    DeliveryRun = __DeliveryController.Get(DeliveryRunUID);

                    if (DeliveryRun.Status == DeliveryStatus.Pending)
                    {
                        DeliveryRun.Status = DeliveryStatus.Started;
                        __DeliveryController.Update(DeliveryRun);

                        __PackageController.UpdateDeliveryRunDropOrder(DeliveryRunUID);

                        List<Package> _Packages = __PackageController.GetPackagesByDelivery(DeliveryRunUID);

                        List<Guid> _CustomerUIDs = _Packages
                            .Select(package => package.CustomerUID)
                            .ToList();

                        List<DeliveryUser> _Customers = __UserController.Get(_CustomerUIDs);

                        foreach(Package _Package in _Packages)
                        {
                            DeliveryUser _Customer = _Customers
                                    .Where(customer => customer.Id == _Package.CustomerUID.ToString())
                                    .SingleOrDefault(new DeliveryUser());

                            Email _CustomerNotification = new Email()
                            {
                                Recipient = _Customer.Email,
                                Subject = "Your Order is Out For Delivery!",
                                Body = "Hi " + _Customer.Forename + ",\n\n" 
                                + "Your order of " + _Package.Description  +" is out for delivery!\n\n"
                                + "Keep an eye on your tracking page with the code: " + _Package.TrackingCode + "\n\n"
                                + "Thanks, Delivery+ Software Solutions"
                            };

                            __EmailController.SendEmail(_CustomerNotification);
                        }
                    }

                    CurrentDrop = __PackageController
                       .GetByDeliveryAndDropNumber(DeliveryRun.UID, DeliveryRun.CurrentDrop);

                    CurrentStopCustomer = __UserController.Get(CurrentDrop.CustomerUID);
                }
                else
                {
                    //INVALID TRACKING CODE ERROR
                }

                return Page();
            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public int GetTotalNumberOfDrops(Guid delivery_uid)
        {
            return __PackageController.GetPackageCountByDelivery(delivery_uid);
        }

        public async Task<IActionResult> OnGetCompleteDrop (Guid uid, int dropNumber)
        {
            Package _CompletedDrop = __PackageController
                .GetByDeliveryAndDropNumber(uid, dropNumber);

            _CompletedDrop.IsDelivered = true;
            __PackageController.Update(_CompletedDrop);

            Business.Delivery.Delivery _CurrentDelivery = __DeliveryController.Get(uid);

            int _PackageCount = __PackageController.GetPackageCountByDelivery(uid);

            if(_CurrentDelivery.CurrentDrop + 1 > _PackageCount)
            {
                _CurrentDelivery.Status = DeliveryStatus.Completed;
                __DeliveryController.Update(_CurrentDelivery);

                return RedirectToPage("DeliveryRuns");
            }
            else
            {
                _CurrentDelivery.CurrentDrop += 1;
                __DeliveryController.Update(_CurrentDelivery);

                return RedirectToPage("CurrentDelivery", new { DeliveryRunUID = uid });
            }
        }


        [BindProperty(SupportsGet = true)]
        public Package CurrentDrop { get; set; }

        [BindProperty(SupportsGet = true)]
        public DeliveryUser CurrentStopCustomer { get; set; }

        [BindProperty(SupportsGet = true)]
        public Business.Delivery.Delivery DeliveryRun { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid DeliveryRunUID { get; set; }
    }
}

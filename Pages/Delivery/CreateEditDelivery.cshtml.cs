using DeliverySoftware.Business.Delivery;
using DeliverySoftware.Business.Fleet;
using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DeliverySoftware.Pages.Delivery
{
    public class CreateEditDeliveryModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";

        private readonly IDeliveryController __DeliveryController;
        private readonly IUserController __UserController;
        private readonly IVanController __VanController;


        public CreateEditDeliveryModel ()
        {
            __UserController = new UserController();
            __DeliveryController = new DeliveryController();
            __VanController = new VanController();
        }

        private void CreateVanSelector ()
        {
            List<Van> _Vans = __VanController.GetAll();

            VanSelection = _Vans.Select(van =>
                new SelectListItem
                {
                    Text = van.Registration,
                    Value = van.UID.ToString(),
                    Selected = DeliveryRun.VanUID.Equals(van.UID)
                }).ToList();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {
                if (UID != null && UID != Guid.Empty)
                {
                    DeliveryRun = __DeliveryController.Get(UID);
                }
                else
                {
                    DeliveryRun = new Business.Delivery.Delivery();
                    DeliveryRun.Date = DateTime.Now.Date;
                }

                CreateVanSelector();

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
                CreateVanSelector();
                return Page();
            }

            if (UID != null && UID != Guid.Empty)
            {
                __DeliveryController.Update(DeliveryRun);
            }
            else
            {
                __DeliveryController.Create(DeliveryRun);
            }

            return RedirectToPage("DeliveryManagement");
        }

        private void ValidateModel ()
        {

            if (DeliveryRun.UID == Guid.Empty)
            {
                if (DeliveryRun.Date < DateTime.Now)
                {
                    ModelState.AddModelError("DeliveryRun.Date", "Can't create a delivery in the past!");
                }
            }
            else
            {
                Business.Delivery.Delivery _CurrentDelivery = __DeliveryController.Get(DeliveryRun.UID);

                if (_CurrentDelivery.Date != DeliveryRun.Date && DeliveryRun.Date < DateTime.Now)
                {
                    ModelState.AddModelError("DeliveryRun.Date", "Can't change a delivery date to the past!");
                }
            }

        }

        [BindProperty(SupportsGet = true)]
        public Business.Delivery.Delivery DeliveryRun { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }

        public List<SelectListItem> VanSelection { get; set; }
    }
}

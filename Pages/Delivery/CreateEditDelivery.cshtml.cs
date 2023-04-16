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
        private readonly IUserController __UserController;
        private readonly IDeliveryController __DeliveryController;
        private readonly IVanController __VanController;


        public CreateEditDeliveryModel ()
        {
            __UserController = new UserController();
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
                    DeliveryRun = __DeliveryController.Get(UID);
                }
                else
                {
                    DeliveryRun = new Business.Delivery.Delivery();
                }

                CreateVanSelector();

                return Page();

            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
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

        public IActionResult OnPost ()
        {
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

        [BindProperty(SupportsGet = true)]
        public Business.Delivery.Delivery DeliveryRun { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
        public List<SelectListItem> VanSelection { get; set; }
    }
}

using DeliverySoftware.Business.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace DeliverySoftware.Pages.User
{
    public class CreateEditUserModel : PageModel
    {
        private const string PERMISSION_DENIED_PAGE_PATH = "../PermissionDenied";
        private readonly IUserController __UserController;

        public CreateEditUserModel()
        {
            __UserController = new UserController();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            DeliveryUser _CurrentUser = __UserController.Get(new Guid(_CurrentUserID));

            if (_CurrentUser.UserType == UserType.Employee)
            {
                if (UID != null && UID != Guid.Empty)
                {
                    ModifiedUser = __UserController.Get(UID);
                    UserTypeToCreateOrEdit = ModifiedUser.UserType;
                }
                else
                {
                    ModifiedUser = new DeliveryUser();
                }

                return Page();

            }
            else
            {
                return RedirectToPage(PERMISSION_DENIED_PAGE_PATH);
            }
        }

        public IActionResult OnPost ()
        {
            if (UID != null && UID != Guid.Empty)
            {
                __UserController.Update(ModifiedUser);
            }
            else
            {
                ModifiedUser.UserType = UserTypeToCreateOrEdit;
                __UserController.Create(ModifiedUser);
            }

            switch(UserTypeToCreateOrEdit)
            {
                case UserType.Driver:
                    return RedirectToPage("../Driver/DriverManagement");
                case UserType.Customer:
                    return RedirectToPage("../Customer/CustomerManagement");
                default:
                    return RedirectToPage("../Index");
            }
        }

        [BindProperty(SupportsGet = true)]
        public DeliveryUser ModifiedUser { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }

        [BindProperty(SupportsGet = true)]
        public UserType UserTypeToCreateOrEdit { get; set; }
    }
}

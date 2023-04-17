using DeliverySoftware.Business.Fleet;
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
            ValidateModel();

            if (!ModelState.IsValid)
            {
                return Page();
            }

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

        private void ValidateModel()
        {
            if (ModifiedUser.UserType == UserType.Customer || UserTypeToCreateOrEdit == UserType.Customer)
            {
                if(ModifiedUser.HouseNumber <= 0)
                {
                    ModelState.AddModelError("ModifiedUser.HouseNumber", "Invalid House Number!");
                }
                if (!ModifiedUser.HouseNumber.HasValue)
                {
                    ModelState.AddModelError("ModifiedUser.HouseNumber", "House Number is Required!");
                }
                if (ModifiedUser.Address == null || ModifiedUser.Address.Length < 1)
                {
                    ModelState.AddModelError("ModifiedUser.Address", "Address is Required!");
                }
                if (ModifiedUser.PostCode == null || ModifiedUser.PostCode.Length < 1)
                {
                    ModelState.AddModelError("ModifiedUser.PostCode", "PostCode is Required!");
                }

            }
            if(ModifiedUser.UserType == UserType.Driver || UserTypeToCreateOrEdit == UserType.Driver)
            {
                if (ModifiedUser.UserName == null || ModifiedUser.UserName.Length < 1)
                {
                    ModelState.AddModelError("ModifiedUser.UserName", "UserName is Required!");
                }
            }

            if (ModifiedUser.Email == null || ModifiedUser.Email.Length < 1)
            {
                ModelState.AddModelError("ModifiedUser.Email", "Email is Required!");
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

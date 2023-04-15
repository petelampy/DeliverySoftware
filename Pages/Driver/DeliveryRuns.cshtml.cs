using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DeliverySoftware.Pages.Driver
{
    [Authorize]
    public class DeliveryRunsModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevenueCompanion.Presentation.Pages.CreditNote
{
    public class AutomaticModel : PageModel
    {
        public void OnGet(string menuSetupId)
        {
            ViewData["menuSetupId"] = menuSetupId;
        }
    }
}

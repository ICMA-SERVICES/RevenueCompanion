using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RevenueCompanion.Presentation.Pages.Administration
{
    public class ManageUserModel : PageModel
    {
        public IActionResult OnGet()
        {
            string userId = HttpContext.Request.Query["userId"].ToString();
            string roleId = HttpContext.Request.Query["roleId"].ToString();
            string email = HttpContext.Request.Query["email"].ToString();
            ViewData["roleId"] = roleId;
            ViewData["selectedUserId"] = userId;
            HttpContext.Session.SetString("userEmail", email);
            return Page();
        }
    }
}

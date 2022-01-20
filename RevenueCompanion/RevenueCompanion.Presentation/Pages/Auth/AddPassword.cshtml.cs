using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.DTOs.Base;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Auth
{
    public class AddPasswordModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public AddPasswordModel(IHttpClientService httpClientService, IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 digit long")]
        [BindProperty]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password does not match")]
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string Code { get; set; }
        public IActionResult OnGet(string code, string userId)
        {
            var passwordAdded = HttpContext.Session.GetString("passwordAdded");
            if (passwordAdded != null)
            {
                ViewData["passwordAdded"] = "Yes";
                HttpContext.Session.Remove("passwordAdded");
                return Page();
            }
            if (code == null || userId == null)
                ViewData["BadRequest"] = "Yes";
            Code = code;
            UserId = userId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ApplicationContants.ValidatePassword(Password))
                {
                    ModelState.AddModelError("", "Password does not meet the rules.");
                    return Page();
                }
                var result = await _httpClientService.GetAsync<BaseDTO<string>>($"{_settings.BaseUrl}{ApplicationContants.confirm_email}?userId={UserId}&code={Code}&password={Password}");
                if (result.succeeded)
                {
                    HttpContext.Session.SetString("passwordAdded", "yes");
                    return RedirectToPage("/Auth/AddPassword");
                }
                ModelState.AddModelError("", "verification failed");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occured while processign your request");
                return Page();
            }
        }
    }
}

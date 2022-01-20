using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.DTOs.Authentication;
using RevenueCompanion.Presentation.DTOs.Base;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public ResetPasswordModel(IHttpClientService httpClientService,
                                    IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Code { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [BindProperty]
        public string ConfirmPassword { get; set; }
        public IActionResult OnGet(string code, string email)
        {
            ViewData["Action"] = "Forgot Password";
            if (!string.IsNullOrEmpty(code) || string.IsNullOrEmpty(email))
            {
                Code = code;
                Email = email;
            }
            ViewData["BadRequest"] = "BadRequest";
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var resetPasswordModel = new ResetPasswordRequestDTO { Email = Email, Token = Code, ConfirmPassword = ConfirmPassword, Password = Password };

            var response = await _httpClientService.PostAsync<ResetPasswordRequestDTO, BaseDTO<string>>($"{_settings.BaseUrl}{ApplicationContants.reset_password}", resetPasswordModel);

            if (response.succeeded)
                ViewData["Successful"] = "Yes";

            return Page();

        }

    }
}

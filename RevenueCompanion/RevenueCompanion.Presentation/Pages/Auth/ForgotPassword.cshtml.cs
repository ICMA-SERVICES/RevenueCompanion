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
    public class ForgotPasswordModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public ForgotPasswordModel(IHttpClientService httpClientService,
                                    IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        [Required]
        [BindProperty]
        public string Email { get; set; }
        public IActionResult OnGet()
        {
            ViewData["Action"] = "Forgot Password";
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var siteLocation = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var forgotPasswordRequest = new ForgotPasswordRequestDTO { Email = Email, WebUrl = siteLocation };

            var response = await _httpClientService.PostAsync<ForgotPasswordRequestDTO, BaseDTO<string>>($"{_settings.BaseUrl}{ApplicationContants.forgot_Password}", forgotPasswordRequest);
            ViewData["Success"] = "Yes";
            //either succeeded or not please return page;
            return Page();

        }
    }
}

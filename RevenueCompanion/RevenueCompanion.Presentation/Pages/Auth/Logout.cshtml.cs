using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.DTOs.Base;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public LogoutModel(IHttpClientService httpClientService, IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClientService.GetAsync<BaseDTO<string>>($"{_settings.BaseUrl}{ApplicationContants.LogOut}");

            if (response.succeeded)
            {
                //var authenticationManager = Request.HttpContext;
                //await authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //foreach (var cookie in Request.Cookies.Keys)
                //{
                //    Response.Cookies.Delete(cookie);
                //}

                return RedirectToPage("/Auth/Login");
            }
            return RedirectToPage("/Auth/Login");
        }
    }
}

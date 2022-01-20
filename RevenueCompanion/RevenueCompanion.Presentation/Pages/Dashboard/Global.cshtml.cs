using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages
{
    public class GlobalModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        private readonly MerchantConfiguration _merchantConfig;
        public GlobalModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings,
                          IOptions<MerchantConfiguration> merchantConfig)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
            _merchantConfig = merchantConfig.Value;
        }
        public int TotalUserCount { get; set; }
        public int TotalApprovalSettingCount { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var loggedInUserId = HttpContext.Session.GetString("userId");
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(loggedInUserId))
            {
                return RedirectToPage("/Auth/Login");
            }
            TotalUserCount = await GetTotalUsers(loggedInUserId, jwtToken);
            TotalApprovalSettingCount = await GetTotalApprovalSettingCount(jwtToken);
            return Page();
        }

        public async Task<int> GetTotalUsers(string userId, string jwtToken)
        {
            var response = await _httpClientService.GetAsync<int>($"{_settings.BaseUrl}{ApplicationContants.GetUserCount}{userId}", jwtToken);
            return response;
        }
        public async Task<int> GetTotalApprovalSettingCount(string jwtToken)
        {
            var response = await _httpClientService.GetAsync<int>($"{_settings.BaseUrl}{ApplicationContants.GetApprovalCount}", jwtToken);
            return response;
        }
    }
}

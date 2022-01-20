using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.MenuSetup;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Setting
{
    public class ApprovalModel : PageModel
    {
        public string MenuSetupId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public List<MenuSetupDTO> Menus { get; set; }
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public ApprovalModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        public async Task OnGet()
        {
            Menus = await GetMenus();
        }

        private async Task<List<MenuSetupDTO>> GetMenus()
        {
            var token = HttpContext.Session.GetString("jwtToken");
            var response = await _httpClientService.GetAsync<Response<List<MenuSetupDTO>>>($"{_settings.BaseUrl}{ApplicationContants.GetMenusWithApprovalSettingsSetAsTrue}", token);
            return response.Data;
        }
    }
}

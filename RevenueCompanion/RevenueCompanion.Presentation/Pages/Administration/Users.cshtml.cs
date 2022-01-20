using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.DTOs.Base;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Administration
{
    public class UsersModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public UsersModel(IHttpClientService httpClientService, IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        public List<RolesDTO> Roles { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var siteLocation = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            HttpContext.Session.SetString("webUrl", siteLocation);
            var token = HttpContext.Session.GetString("jwtToken");
            var response = await _httpClientService.GetAsync<BaseDTO<List<RolesDTO>>>($"{_settings.BaseUrl}{ApplicationContants.GetRoles}", token);
            if (response.succeeded)
            {
                Roles = response.data;
            }

            return Page();
        }
    }
}

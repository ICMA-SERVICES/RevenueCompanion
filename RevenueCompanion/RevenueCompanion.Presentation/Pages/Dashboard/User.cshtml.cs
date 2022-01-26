using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Dashboard
{
    public class UserModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        private readonly MerchantConfiguration _merchantConfig;
        public UserModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings,
                          IOptions<MerchantConfiguration> merchantConfig)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
            _merchantConfig = merchantConfig.Value;
        }
        public int TotalRequest { get; set; }
        public int TotalApproved { get; set; }
        public int TotalNotApproved { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var loggedInUserRole = HttpContext.Session.GetString("role");
            var loggedInUserId = HttpContext.Session.GetString("userId");
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            if (string.IsNullOrEmpty(loggedInUserId) || string.IsNullOrEmpty(loggedInUserRole))
            {
                return RedirectToPage("/Auth/Login");
            }
            if (loggedInUserRole == "Maker")
            {
                var creditNoteList = await GetCreditNoteListByUserId(loggedInUserId, jwtToken);
                TotalRequest = creditNoteList.Count();
                TotalApproved = creditNoteList.Where(c => c.IsApproved == true).Count();
                TotalNotApproved = creditNoteList.Where(c => c.IsApproved == false).Count();
            }
            else
            {
                var creditNoteList = await GetCreditNotesNotAttendedToByUserId(loggedInUserId, jwtToken);
                TotalNotApproved = creditNoteList.Count();
            }
            return Page();
        }

        private async Task<List<CreditNoteRequestDTO>> GetCreditNoteListByUserId(string userId, string jwtToken)
        {
            var response = await _httpClientService.GetAsync<Response<List<CreditNoteRequestDTO>>>($"{_settings.BaseUrl}{ApplicationContants.GetCreditNotRequestByUser}{userId}", jwtToken);
            return response.Data;
        }
        private async Task<List<CreditNoteRequestDTO>> GetCreditNotesNotAttendedToByUserId(string userId, string jwtToken)
        {
            var response = await _httpClientService.GetAsync<Response<List<CreditNoteRequestDTO>>>($"{_settings.BaseUrl}{ApplicationContants.GetNotesNotAttendedToByUserId}{userId}", jwtToken);
            return response.Data;
        }

    }
}

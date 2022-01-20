using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.CreditNote
{
    public class RequestModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public RequestModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }
        public List<CreditNoteRequestDTO> RequestList { get; set; }
        public async Task OnGet()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var jwtToken = HttpContext.Session.GetString("jwtToken");
            RequestList = await GetCreditNotesRequestedByMe(userId, jwtToken);
        }
        private async Task<List<CreditNoteRequestDTO>> GetCreditNotesRequestedByMe(string userId, string jwtToken)
        {
            var response = await _httpClientService.GetAsync<Response<List<CreditNoteRequestDTO>>>($"{_settings.BaseUrl}{ApplicationContants.GetCreditNotRequestByUserId}{userId}", jwtToken);
            if (response is null)
            {
                return new List<CreditNoteRequestDTO>();
            }
            return response.Data;
        }
    }
}

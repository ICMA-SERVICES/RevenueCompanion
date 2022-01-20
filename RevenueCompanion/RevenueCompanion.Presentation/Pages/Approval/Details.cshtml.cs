using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Presentation.Constants;
using RevenueCompanion.Presentation.Extensions;
using RevenueCompanion.Presentation.Services.Interface;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Pages.Approval
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ApiConfiguration _settings;
        public DetailsModel(IHttpClientService httpClientService,
                          IOptions<ApiConfiguration> settings)
        {
            _httpClientService = httpClientService;
            _settings = settings.Value;
        }

        public CreditNoteDetail CreditNoteDetail { get; set; }
        public async Task OnGet(int requestId)
        {
            var token = HttpContext.Session.GetString("jwtToken");
            CreditNoteDetail = await GetCreditNoteDetails(requestId, token);
        }
        private async Task<CreditNoteDetail> GetCreditNoteDetails(int creditNoteId, string token)
        {
            var response = await _httpClientService.GetAsync<Response<CreditNoteDetail>>($"{_settings.BaseUrl}{ApplicationContants.GetCreditNotRequestDetails}{creditNoteId}", token);
            if (response is null)
            {
                return new CreditNoteDetail();
            }
            return response.Data;
        }
    } 
}

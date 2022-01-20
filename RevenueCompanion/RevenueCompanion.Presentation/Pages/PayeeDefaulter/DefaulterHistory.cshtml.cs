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

namespace RevenueCompanion.Presentation.Pages.SummaryReport
{
    public class DefaulterHistoryModel : PageModel
    {
        public async Task OnGet()
        {
        }
    }
}

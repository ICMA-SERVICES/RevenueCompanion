using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.DTOs.SummaryReport;
using RevenueCompanion.Application.Features.CreditNote.Commands;
using RevenueCompanion.Application.Features.CreditNote.Queries;
using RevenueCompanion.Application.Helpers;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class SummaryReportController : BaseApiController
    {
        private readonly ISummaryReportRepository _summaryReportRepository;

        public SummaryReportController(ISummaryReportRepository summaryReportRepository)
        {
            _summaryReportRepository = summaryReportRepository;
        }

        [HttpGet("summary-report")]
        [ProducesResponseType(typeof(List<SummaryReportDTO>), 200)]
        public IActionResult GetSummaryReport(DataSourceLoadOptions loadOptions)
        {
            var year = Request.Headers["year"];
            var result = _summaryReportRepository.GetSummaryReport(year);
            loadOptions.PrimaryKey = new[] { $"PayerUtin" };
            return Ok(DataSourceLoader.Load(result, loadOptions));
        }

        [HttpGet("summary-details")]
        [ProducesResponseType(typeof(List<SummaryReportDTO>), 200)]
        public IActionResult GetSummaryDetails(DataSourceLoadOptions loadOptions)
        {
            var payerUtin = Request.Headers["payerUtin"];
            var result = _summaryReportRepository.GetSummaryDetails(payerUtin);
            loadOptions.PrimaryKey = new[] { $"PayerUtin" };
            return Ok(DataSourceLoader.Load(result, loadOptions));
        }

        [HttpGet("payer-list")]
        [ProducesResponseType(typeof(List<SummaryReportDTO>), 200)]
        public IActionResult GetPayerList(DataSourceLoadOptions loadOptions)
        {
            var result = _summaryReportRepository.GetPayerList();
            return Ok(result);
        }

        [HttpGet("defaulter-history")]
        [ProducesResponseType(typeof(List<SummaryReportDTO>), 200)]
        public IActionResult GetDefaulterHistory(DataSourceLoadOptions loadOptions)
        {
            var payerUtin = Request.Headers["payerUtin"];
            var year = Request.Headers["year"];
            var result = _summaryReportRepository.GetDefaultersHistory(payerUtin, year);
            loadOptions.PrimaryKey = new[] { $"PayerUtin" };
            return Ok(DataSourceLoader.Load(result, loadOptions));
        }

    }
}

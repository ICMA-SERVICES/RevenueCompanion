using RevenueCompanion.Application.DTOs.SummaryReport;
using System.Collections.Generic;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface ISummaryReportRepository
    {
        List<SummaryReportDTO> GetDefaultersHistory(string payerUtin, string year);
        List<SummaryReportDTO> GetSummaryDetails(string payerUtin);
        List<SummaryReportDTO> GetSummaryReport(string year);
        List<SummaryReportDTO> GetPayerList();
    }
}
using System;

namespace RevenueCompanion.Application.DTOs.SummaryReport
{
    public class SummaryReportDTO
    {
        public string TaxAgent { get; set; }
        public int TotalDaysDefualted { get; set; }
        public double PaymentAmount { get; set; }
        public double TotalPaymentAmount { get; set; }
        public string PayerUtin { get; set; }
        public string MonthDue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ActualDateOfPayment { get; set; }
        public int NoOfDaysDefaulted { get; set; }
        public double InitialAmountDue { get; set; }
        public double AccruedIntreset { get; set; }
        public double AccruedPenalty { get; set; }
        public double TotalLiability { get; set; }
        public double LiabilityPaid { get; set; }
        public double Balance { get; set; }
        public string AssessmentRefNo { get; set; }
        public double PeriodAmount { get; set; }
    }
}
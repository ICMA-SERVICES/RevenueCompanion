using System;

namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class SearchFromAssessmentDTO
    {
        public long AssessmentId { get; set; }
        public string AssessmentRefNo { get; set; }
        public string MerchantCode { get; set; }
        public string Location { get; set; }
        public string PayerName { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Narration { get; set; }
        public string RevenueCode { get; set; }
        public string RevenueName { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string Platformcode { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? AssessmentBalance { get; set; }
        public bool? PartPaymentAllow { get; set; }
        public long? ParentId { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsReversed { get; set; }
        public string Reversedby { get; set; }
        public DateTime? DateReversed { get; set; }
        public string AgentUtin { get; set; }
        public string TaxYear { get; set; }
        public string PreviousYearAssessmentRefNo { get; set; }
        public string AssessmentCreatedBy { get; set; }
        public DateTime? AssessmentCreatedDate { get; set; }
        public string AssessmentApprovedBy { get; set; }
        public DateTime? AssessmentDateApproved { get; set; }
        public bool? PushtoXpress { get; set; }
        public DateTime? DatePushtoXpress { get; set; }
    }
}

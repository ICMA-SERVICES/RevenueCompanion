using System;

namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class CreditNoteRequestDTO
    {
        public int CreditNoteRequestId { get; set; }
        public int ApprovalSettingId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public int ApprovalCount { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string AssessmentReferenceNumber { get; set; }
        public DateTime DateRequested { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateApproved { get; set; }
        public double ActualAmount { get; set; }
        public string RequestedBy { get; set; }
        public double AmountUsed { get; set; }
        public double Balance { get; set; }
        public string AppCode { get; set; }
        public string MerchantCode { get; set; }
    }
}

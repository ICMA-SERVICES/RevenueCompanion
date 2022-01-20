using System;

namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class CreateCreditNoteRequestDTO
    {
        public int MenuSetupId { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string AssessmentReferenceNumber { get; set; }
        public DateTime DateRequested { get; set; }
        public double ActualAmount { get; set; }
        public double AmountUsed { get; set; }
        public double Balance { get; set; }
        public string MerchantCode { get; set; }
        public string TransType { get; set; }
    }
}

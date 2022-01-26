using System;
using System.Collections.Generic;

namespace RevenueCompanion.Domain.Entities
{
    public class CreditNoteRequest : BaseEntity
    {
        public int CreditNoteRequestId { get; set; }
        public int ApprovalSettingId { get; set; }
        public int NoOfRequiredApproval { get; set; }
        public int ApprovalCount { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string AssessmentReferenceNumber { get; set; }
        public string RequestedByName { get; set; }
        public string RequestedByEmail { get; set; }
        public string RequestedById { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? IsApproved { get; set; } = null;
        public DateTime DateApproved { get; set; }
        public double ActualAmount { get; set; }
        public double AmountUsed { get; set; }
        public double Balance { get; set; }
        public int CreditNoteRequestTypeId { get; set; }

        public CreditNoteRequestType CreditNoteRequestType { get; set; }
        public List<CreditNoteRequestApprovalDetail> CreditNoteRequestApprovalDetails { get; set; } = new List<CreditNoteRequestApprovalDetail>();

    }
}

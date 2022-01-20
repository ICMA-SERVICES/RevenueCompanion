using System;

namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class CreditNoteRequestApprovalDetailDTO
    {
        public int CreditNoteRequestApprovalDetailId { get; set; }
        public int CreditNoteRequestId { get; set; }
        public bool IsApproved { get; set; }
        public string ActedUponBy { get; set; }
        public DateTime ActedUponOn { get; set; }
        public string ApproverUserId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }


}

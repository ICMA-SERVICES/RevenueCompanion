namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class CreateCreditNoteRequestDetailDTO
    {
        public int CreditNoteRequestId { get; set; }
        public bool IsApproved { get; set; }
        public string ActedUponBy { get; set; }
        public string ApproverUserId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}

namespace RevenueCompanion.Domain.Entities
{
    public class CreditNoteRequestApprovalDetail : BaseEntity
    {
        public int CreditNoteRequestApprovalDetailId { get; set; }
        public int CreditNoteRequestId { get; set; }
        public bool IsApproved { get; set; }
        public string ActedUponBy { get; set; }
        public string ApproverUserId { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public CreditNoteRequest CreditNoteRequest { get; set; }
    }
}

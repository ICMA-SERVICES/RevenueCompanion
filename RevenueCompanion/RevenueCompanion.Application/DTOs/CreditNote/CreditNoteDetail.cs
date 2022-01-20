using RevenueCompanion.Domain.Entities;
using System.Collections.Generic;

namespace RevenueCompanion.Application.DTOs.CreditNote
{
    public class CreditNoteDetail
    {
        public CreditNoteRequest CreditNoteRequest { get; set; }
        public SearchFromReconciliationDTO SearchFromReconciliationDTO { get; set; }
        public SearchFromAssessmentDTO SearchFromAssessmentDTO { get; set; }
        public List<CreditNoteRequestApprovalDetailDTO> CreditNoteRequestApprovalDetails { get; set; }
    }
}

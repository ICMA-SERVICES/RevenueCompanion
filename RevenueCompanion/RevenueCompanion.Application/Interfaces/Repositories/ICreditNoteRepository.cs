using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface ICreditNoteRepository
    {
        Task<RepositoryResponseDTO<SearchFromReconciliationDTO>> SearchByPaymentRef(string paymentRef);
        Task<SearchFromAssessmentDTO> GetAssessmentInfoByAssessmentRef(string assessmentRef);
        Task<bool> AddCreditNoteRequest(CreateCreditNoteRequestDTO request);
        Task<List<CreditNoteRequestDTO>> GetCreditNoteRequestListByUserId(string userId);
        Task<CreditNoteRequest> GetCreditNoteRequestById(int id);
        Task<List<CreditNoteRequestDTO>> GetCreditNoteRequestsNotAttendedToByUser(string userId);
        Task<CreditNoteDetail> GetCreditNoteRequestDetailsByCreditRequestId(int creditRequestId);
        Task<RepositoryResponseDTO<bool>> ApproveCreditNoteRequest(int creditRequestId, bool isApproved, string comment = "Satisfactorily");
        Task<RepositoryResponseDTO<bool>> DisApproveCreditNoteRequest(int creditRequestDetailId, string comment);

    }
}

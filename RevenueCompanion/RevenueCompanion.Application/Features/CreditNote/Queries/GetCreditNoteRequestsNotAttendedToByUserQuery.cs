using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Queries
{
    public class GetCreditNoteRequestsNotAttendedToByUserQuery : IRequest<Response<List<CreditNoteRequestDTO>>>
    {
        public string UserId { get; set; }
    }
    public class GetCreditNoteRequestsNotAttendedToByUserQueryHandler : IRequestHandler<GetCreditNoteRequestsNotAttendedToByUserQuery, Response<List<CreditNoteRequestDTO>>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;

        public GetCreditNoteRequestsNotAttendedToByUserQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<List<CreditNoteRequestDTO>>> Handle(GetCreditNoteRequestsNotAttendedToByUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.GetCreditNoteRequestsNotAttendedToByUser(request.UserId);
            if (response != null)
                return ApplicationConstants.SuccessMessage<List<CreditNoteRequestDTO>>(response, "success");

            return ApplicationConstants.FailureMessage<List<CreditNoteRequestDTO>>(null, "No record found");
        }
    }
}

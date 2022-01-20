using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Queries
{
    public class GetCreditNoteRequestDetailsByCreditRequestIdQuery : IRequest<Response<CreditNoteDetail>>
    {
        public int CreditRequestId { get; set; }
    }
    public class GetCreditNoteRequestDetailsByCreditRequestIdQueryHandler : IRequestHandler<GetCreditNoteRequestDetailsByCreditRequestIdQuery, Response<CreditNoteDetail>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;
        public GetCreditNoteRequestDetailsByCreditRequestIdQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<CreditNoteDetail>> Handle(GetCreditNoteRequestDetailsByCreditRequestIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.GetCreditNoteRequestDetailsByCreditRequestId(request.CreditRequestId);
            if (response != null)
                return ApplicationConstants.SuccessMessage<CreditNoteDetail>(response, "success");

            return ApplicationConstants.FailureMessage<CreditNoteDetail>(null, "No record found");
        }
    }
}

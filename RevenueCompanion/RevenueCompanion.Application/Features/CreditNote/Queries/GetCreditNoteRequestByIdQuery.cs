using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using RevenueCompanion.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Queries
{
    public class GetCreditNoteRequestByIdQuery : IRequest<Response<CreditNoteRequest>>
    {
        public int Id { get; set; }
    }
    public class GetCreditNoteRequestByIdQueryHandler : IRequestHandler<GetCreditNoteRequestByIdQuery, Response<CreditNoteRequest>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;

        public GetCreditNoteRequestByIdQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<CreditNoteRequest>> Handle(GetCreditNoteRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.GetCreditNoteRequestById(request.Id);
            if (response != null)
                return ApplicationConstants.SuccessMessage<CreditNoteRequest>(response, "success");

            return ApplicationConstants.FailureMessage<CreditNoteRequest>(null, "No record found");
        }
    }
}

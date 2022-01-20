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
    public class GetCreditNoteRequestListQuery : IRequest<Response<List<CreditNoteRequestDTO>>>
    {
        public string UserId { get; set; }
    }
    public class GetCreditNoteRequestListQueryHandler : IRequestHandler<GetCreditNoteRequestListQuery, Response<List<CreditNoteRequestDTO>>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;

        public GetCreditNoteRequestListQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<List<CreditNoteRequestDTO>>> Handle(GetCreditNoteRequestListQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.GetCreditNoteRequestListByUserId(request.UserId);
            if (response != null)
                return ApplicationConstants.SuccessMessage<List<CreditNoteRequestDTO>>(response, "success");

            return ApplicationConstants.FailureMessage<List<CreditNoteRequestDTO>>(null, "No record found");
        }
    }
}

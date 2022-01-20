using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Queries
{
    public class SearchByPaymentRefQuery : IRequest<Response<SearchFromReconciliationDTO>>
    {
        public string PaymentRefNo { get; set; }
    }
    public class SearchByPaymentRefQueryHandler : IRequestHandler<SearchByPaymentRefQuery, Response<SearchFromReconciliationDTO>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;

        public SearchByPaymentRefQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<SearchFromReconciliationDTO>> Handle(SearchByPaymentRefQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.SearchByPaymentRef(request.PaymentRefNo);
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage<SearchFromReconciliationDTO>(response.Data, "success");

            return ApplicationConstants.FailureMessage<SearchFromReconciliationDTO>(null, response.Message);
        }
    }

}

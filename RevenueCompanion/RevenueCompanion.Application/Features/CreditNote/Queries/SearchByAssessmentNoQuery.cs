using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Queries
{
    public class SearchByAssessmentNoQuery : IRequest<Response<SearchFromAssessmentDTO>>
    {
        public string AssessmentRefNo { get; set; }
    }
    public class SearchByAssessmentNoQueryHandler : IRequestHandler<SearchByAssessmentNoQuery, Response<SearchFromAssessmentDTO>>
    {
        private readonly ICreditNoteRepository _creditNoteRepository;

        public SearchByAssessmentNoQueryHandler(ICreditNoteRepository creditNoteRepository)
        {
            _creditNoteRepository = creditNoteRepository;
        }
        public async Task<Response<SearchFromAssessmentDTO>> Handle(SearchByAssessmentNoQuery request, CancellationToken cancellationToken)
        {
            var response = await _creditNoteRepository.GetAssessmentInfoByAssessmentRef(request.AssessmentRefNo);
            if (response != null)
                return ApplicationConstants.SuccessMessage<SearchFromAssessmentDTO>(response, "success");

            return ApplicationConstants.FailureMessage<SearchFromAssessmentDTO>(null, "No record found");
        }
    }
}

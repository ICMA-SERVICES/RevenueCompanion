using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Commands
{
    public class DisapproveCreditNoteRequestCommand : IRequest<Response<string>>
    {
        public int CreditRequestId { get; set; }
        public string Comment { get; set; }
    }
    public class DisapproveCreditNoteRequestCommandHandler : IRequestHandler<DisapproveCreditNoteRequestCommand, Response<string>>
    {
        private readonly ICreditNoteRepository _creditNote;

        public DisapproveCreditNoteRequestCommandHandler(ICreditNoteRepository creditNote)
        {
            _creditNote = creditNote;
        }
        public async Task<Response<string>> Handle(DisapproveCreditNoteRequestCommand request, CancellationToken cancellationToken)
        {
            var response = await _creditNote.DisApproveCreditNoteRequest(request.CreditRequestId, request.Comment);
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("Disapproved successfully");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}

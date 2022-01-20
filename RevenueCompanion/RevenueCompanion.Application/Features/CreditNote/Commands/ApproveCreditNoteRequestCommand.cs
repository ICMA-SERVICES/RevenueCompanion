using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Commands
{
    public class ApproveCreditNoteRequestCommand : IRequest<Response<string>>
    {
        public int CreditRequestId { get; set; }
        public bool IsApproved { get; set; }
        public string Comment { get; set; }
    }
    public class ApproveCreditNoteRequestCommandHandler : IRequestHandler<ApproveCreditNoteRequestCommand, Response<string>>
    {
        private readonly ICreditNoteRepository _creditNote;

        public ApproveCreditNoteRequestCommandHandler(ICreditNoteRepository creditNote)
        {
            _creditNote = creditNote;
        }
        public async Task<Response<string>> Handle(ApproveCreditNoteRequestCommand request, CancellationToken cancellationToken)
        {
            var response = await _creditNote.ApproveCreditNoteRequest(request.CreditRequestId, request.IsApproved, request.Comment);
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("Approved successfully");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }

}

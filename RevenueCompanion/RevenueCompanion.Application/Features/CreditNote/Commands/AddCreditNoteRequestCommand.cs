using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.CreditNote;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Features.CreditNote.Commands
{
    public class AddCreditNoteRequestCommand : IRequest<Response<string>>
    {
        public int MenuSetupId { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public string AssessmentReferenceNumber { get; set; }
        public double ActualAmount { get; set; }
        public double AmountUsed { get; set; }
        public double Balance { get; set; }
        public string MerchantCode { get; set; }
        public string TransType { get; set; }
    }
    public class AddCreditNoteRequestCommandHandler : IRequestHandler<AddCreditNoteRequestCommand, Response<string>>
    {
        private readonly ICreditNoteRepository _creditNote;
        public AddCreditNoteRequestCommandHandler(ICreditNoteRepository creditNote)
        {
            _creditNote = creditNote;
        }
        public async Task<Response<string>> Handle(AddCreditNoteRequestCommand request, CancellationToken cancellationToken)
        {
            var isSucceeded = await _creditNote.AddCreditNoteRequest(new CreateCreditNoteRequestDTO
            {
                ActualAmount = request.ActualAmount,
                AmountUsed = request.AmountUsed,
                MenuSetupId = request.MenuSetupId,
                Balance = request.Balance,
                DateRequested = DateTime.Now,
                MerchantCode = request.MerchantCode,
                PaymentReferenceNumber = request.PaymentReferenceNumber,
                AssessmentReferenceNumber = request.AssessmentReferenceNumber,
                TransType = request.TransType
            });
            if (isSucceeded)
                return ApplicationConstants.SuccessMessage("Added succesfully");
            return ApplicationConstants.FailureMessage("Failed");
        }
    }
}

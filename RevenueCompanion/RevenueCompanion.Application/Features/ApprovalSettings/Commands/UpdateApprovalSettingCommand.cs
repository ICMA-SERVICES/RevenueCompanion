using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.ApprovalSettings;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
namespace RevenueCompanion.Application.Features.ApprovalSettings.Commands
{
    public class UpdateApprovalSettingCommand : IRequest<Response<string>>
    {
        [Required]
        public int ApprovalSettingId { get; set; }
        [Required]
        public int NoOfRequiredApproval { get; set; }
    }
    public class UpdateApprovalSettingCommandHandler : IRequestHandler<UpdateApprovalSettingCommand, Response<string>>
    {
        private readonly IApprovalSettingRepository _approvalSettingRepository;

        public UpdateApprovalSettingCommandHandler(IApprovalSettingRepository approvalSettingRepository)
        {
            _approvalSettingRepository = approvalSettingRepository;
        }
        public async Task<Response<string>> Handle(UpdateApprovalSettingCommand request, CancellationToken cancellationToken)
        {
            var response = _approvalSettingRepository.UpdateApprovalSetting(new UpdateApprovalSettingDTO
            {
                ApprovalSettingId = request.ApprovalSettingId,
                NoOfRequiredApproval = request.NoOfRequiredApproval,
            });

            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("setting updated successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}

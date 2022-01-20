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
    public class DeleteApprovalSettingsCommand : IRequest<Response<string>>
    {
        [Required]
        public int ApprovalSettingId { get; set; }
    }
    public class DeleteApprovalSettingsCommandHandler : IRequestHandler<DeleteApprovalSettingsCommand, Response<string>>
    {
        private readonly IApprovalSettingRepository _approvalSettingRepository;

        public DeleteApprovalSettingsCommandHandler(IApprovalSettingRepository approvalSettingRepository)
        {
            _approvalSettingRepository = approvalSettingRepository;
        }
        public async Task<Response<string>> Handle(DeleteApprovalSettingsCommand request, CancellationToken cancellationToken)
        {
            var response = _approvalSettingRepository.DeleteApprovalSetting(new DeleteApprovalSettingDTO { ApprovalSettingId = request.ApprovalSettingId });
            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("app deleted successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }

}

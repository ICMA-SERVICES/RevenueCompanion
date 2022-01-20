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
    public class CreateApprovalSettingCommand : IRequest<Response<string>>
    {
        [Required]
        public int MenuSetupId { get; set; }
        [Required]
        public int NoOfRequiredApproval { get; set; }

    }
    public class CreateApprovalSettingCommandHandler : IRequestHandler<CreateApprovalSettingCommand, Response<string>>
    {
        private readonly IApprovalSettingRepository _approvalSettingRepository;

        public CreateApprovalSettingCommandHandler(IApprovalSettingRepository approvalSettingRepository)
        {
            _approvalSettingRepository = approvalSettingRepository;
        }
        public async Task<Response<string>> Handle(CreateApprovalSettingCommand request, CancellationToken cancellationToken)
        {
            var response = _approvalSettingRepository.CreateApprovalSetting(new CreateApprovalSettingDTO
            {
                MenuSetupId = request.MenuSetupId,
                NoOfRequiredApproval = request.NoOfRequiredApproval,
            });

            if (response.Succeeded)
                return ApplicationConstants.SuccessMessage("setting created successfuly");
            return ApplicationConstants.FailureMessage(response.Message);
        }
    }
}

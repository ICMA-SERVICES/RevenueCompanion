using MediatR;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DTOs.ApprovalSettings;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Application.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
namespace RevenueCompanion.Application.Features.ApprovalSettings.Queries
{
    public class GetApprovalSettingByIdQuery : IRequest<Response<ApprovalSettingDTO>>
    {
        [Required]
        public int ApprovalSettingId { get; set; }
    }
    public class GetApprovalSettingByIdQueryHandler : IRequestHandler<GetApprovalSettingByIdQuery, Response<ApprovalSettingDTO>>
    {
        private readonly IApprovalSettingRepository _approvalSettingRepository;

        public GetApprovalSettingByIdQueryHandler(IApprovalSettingRepository approvalSettingRepository)
        {
            _approvalSettingRepository = approvalSettingRepository;
        }
        public async Task<Response<ApprovalSettingDTO>> Handle(GetApprovalSettingByIdQuery request, CancellationToken cancellationToken)
        {
            var response = _approvalSettingRepository.GetApprovalSetting(request.ApprovalSettingId);
            if (response is null)
                return ApplicationConstants.FailureMessage<ApprovalSettingDTO>(response, "failed to retrieve app");
            return ApplicationConstants.SuccessMessage<ApprovalSettingDTO>(response, "success");
        }
    }

}

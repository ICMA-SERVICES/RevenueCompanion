using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.ApprovalSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IApprovalSettingRepository
    {
        RepositoryResponse CreateApprovalSetting(CreateApprovalSettingDTO request);
        RepositoryResponse UpdateApprovalSetting(UpdateApprovalSettingDTO request);
        RepositoryResponse DeleteApprovalSetting(DeleteApprovalSettingDTO request);
        Task<List<ApprovalSettingDTO>> GetApprovalSettings();
        ApprovalSettingDTO GetApprovalSetting(int appId);


    }

}

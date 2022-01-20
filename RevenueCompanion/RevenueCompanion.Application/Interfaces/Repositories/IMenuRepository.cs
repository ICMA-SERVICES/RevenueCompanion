using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.MenuSetup;
using RevenueCompanion.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<List<MenuSetupDTO>> GetMenu(string RoleID);
        Task<List<MenuSetupDTO>> GetPagesAssignedToUser(string UserID, string userPageSecret);
        // Task<string> GetStoreName();
        RepositoryResponse CreateMenuSetup(CreateMenuSetupDTO createMenuDTO);


        Task<int> AssignMenusToUser(MenuToUserRequest model);
        Task<int> DeleteMenuAssignedToUser(MenuToUserRequest model);
        Task<List<MenuSetupDTO>> GetMenusWithApprovalSettingAsTrue();
        Task<MerchantConfig> GetMerchantSetup(string url);
    }
}

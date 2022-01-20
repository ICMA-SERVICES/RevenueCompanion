using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.AppUser;
using System.Collections.Generic;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IAppUserRepository
    {
        RepositoryResponse CreateAppUser(CreateAppUserDTO request);
        RepositoryResponse DisableAppUser(int appUserId);
        RepositoryResponse EnableAppUser(int appUserId);
        //RepositoryResponse UpdateApp(UpdateAppDTO request);
        RepositoryResponse DeleteAppUser(DeleteAppUserDTO request);
        List<AppUserDTO> GetAppUsers(string appCode);
        List<AppUserDTO> GetAll();
        AppUserDTO GetAppUser(int appUserId);

    }
}

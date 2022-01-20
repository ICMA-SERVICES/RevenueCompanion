using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.App;
using System.Collections.Generic;

namespace RevenueCompanion.Application.Interfaces.Repositories
{
    public interface IAppRepository
    {
        RepositoryResponse CreateApp(CreateAppDTO request);
        RepositoryResponse UpdateApp(UpdateAppDTO request);
        RepositoryResponse DeleteApp(DeleteAppDTO request);
        List<AppDTO> GetApps();
        AppDTO GetApp(int appId);
    }
}

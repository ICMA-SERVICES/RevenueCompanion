using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.App;
using RevenueCompanion.Application.Enums;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class AppRepository : IAppRepository
    {
        private readonly ILogger<AppRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        //  private readonly IOptions<ConnectionStrings> _connectionString;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;

        public AppRepository(ILogger<AppRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IDapper dapper)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _dapper = dapper;
            constring = myconnectionString.Value.DefaultConnection;
        }
        public RepositoryResponse CreateApp(CreateAppDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.INSERT);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("MenuUrl", request.MenuUrl);
                param.Add("Code", request.Code);
                param.Add("Name", request.Name);
                param.Add("CreatedBy", _authenticatedUser.UserId);
                var response = _dapper.Insert<int>(ApplicationConstants.Sp_App, param, CommandType.StoredProcedure);
                if (response < 0)
                    return ApplicationConstants.RepositoryExists();
                else if (response == 0)
                    return ApplicationConstants.RepositoryFailed();
                return ApplicationConstants.RepositorySuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return ApplicationConstants.RepositoryFailed();
            }
        }

        public RepositoryResponse DeleteApp(DeleteAppDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.DELETE);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("AppId", request.AppId);
                param.Add("DeletedBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_App, param, CommandType.StoredProcedure);
                if (response > 0)
                    return ApplicationConstants.RepositorySuccess();
                return ApplicationConstants.RepositoryFailed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return ApplicationConstants.RepositoryFailed();
            }
        }

        public AppDTO GetApp(int appId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.GETBYID);
                param.Add("AppId", appId);
                var response = _dapper.Get<AppDTO>(ApplicationConstants.Sp_App, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }

        public List<AppDTO> GetApps()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.GETALL);
                var response = _dapper.GetAll<AppDTO>(ApplicationConstants.Sp_App, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return new List<AppDTO>();
            }
        }

        public RepositoryResponse UpdateApp(UpdateAppDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.UPDATE);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("AppId", request.AppId);
                param.Add("MenuUrl", request.MenuUrl);
                param.Add("Name", request.Name);
                param.Add("Code", request.Code);
                param.Add("UpdatedBy", _authenticatedUser.UserId);
                var response = _dapper.Update<int>(ApplicationConstants.Sp_App, param, CommandType.StoredProcedure);
                if (response > 0)
                    return ApplicationConstants.RepositorySuccess();
                return ApplicationConstants.RepositoryFailed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return ApplicationConstants.RepositoryFailed();
            }
        }
    }
}

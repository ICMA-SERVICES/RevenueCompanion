using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.AppUser;
using RevenueCompanion.Application.Enums;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ILogger<AppUserRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;

        public AppUserRepository(ILogger<AppUserRepository> logger,
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
        public RepositoryResponse CreateAppUser(CreateAppUserDTO request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.INSERT);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("AppCode", request.AppCode);
                param.Add("UserId", request.UserId);
                param.Add("CreatedBy", _authenticatedUser.UserId);
                var response = _dapper.Insert<int>(ApplicationConstants.Sp_AppUser, param, CommandType.StoredProcedure);
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

        public RepositoryResponse DeleteAppUser(DeleteAppUserDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.DELETE);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("AppUserId", request.AppUserId);
                param.Add("DeletedBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_AppUser, param, CommandType.StoredProcedure);
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

        public List<AppUserDTO> GetAll()
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.GETALL);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                var response = _dapper.GetAll<AppUserDTO>(ApplicationConstants.Sp_AppUser, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return new List<AppUserDTO>();
            }
        }

        public AppUserDTO GetAppUser(int appUserId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.GETBYID);
                param.Add("AppUserId", appUserId);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                var response = _dapper.Get<AppUserDTO>(ApplicationConstants.Sp_AppUser, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }

        public List<AppUserDTO> GetAppUsers(string appCode)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", 10);
                param.Add("AppCode", appCode);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                var response = _dapper.GetAll<AppUserDTO>(ApplicationConstants.Sp_AppUser, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return new List<AppUserDTO>();
            }
        }

        public RepositoryResponse DisableAppUser(int appUserId)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", 12);
                param.Add("AppUserId", appUserId);
                param.Add("DisabledBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_AppUser, param, CommandType.StoredProcedure);
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

        public RepositoryResponse EnableAppUser(int appUserId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", 11);
                param.Add("AppUserId", appUserId);
                param.Add("EnabledBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_AppUser, param, CommandType.StoredProcedure);
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

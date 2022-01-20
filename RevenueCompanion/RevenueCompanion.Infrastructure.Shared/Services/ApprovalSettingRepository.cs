using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.ApprovalSettings;
using RevenueCompanion.Application.Enums;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class ApprovalSettingRepository : IApprovalSettingRepository
    {
        private readonly ILogger<ApprovalSettingRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        //  private readonly IOptions<ConnectionStrings> _connectionString;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;
        private readonly ApplicationDbContext _context;

        public ApprovalSettingRepository(ILogger<ApprovalSettingRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IDapper dapper,
                               ApplicationDbContext context)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _dapper = dapper;
            _context = context;
            constring = myconnectionString.Value.DefaultConnection;
        }
        public RepositoryResponse CreateApprovalSetting(CreateApprovalSettingDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.INSERT);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("NoOfRequiredApproval", request.NoOfRequiredApproval);
                param.Add("MenuSetupId", request.MenuSetupId);
                param.Add("CreatedBy", _authenticatedUser.UserId);
                var response = _dapper.Insert<int>(ApplicationConstants.Sp_ApprovalSettings, param, CommandType.StoredProcedure);
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

        public RepositoryResponse DeleteApprovalSetting(DeleteApprovalSettingDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.DELETE);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("ApprovalSettingId", request.ApprovalSettingId);
                param.Add("DeletedBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_ApprovalSettings, param, CommandType.StoredProcedure);
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

        public ApprovalSettingDTO GetApprovalSetting(int approvalSettingId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.GETBYID);
                param.Add("ApprovalSettingId", approvalSettingId);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                var response = _dapper.Get<ApprovalSettingDTO>(ApplicationConstants.Sp_ApprovalSettings, param, commandType: CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }

        //public List<ApprovalSettingDTO> GetApprovalSettings()
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("Status", Status.GETALL);
        //        param.Add("MerchantCode", _authenticatedUser.MerchantCode);
        //        var response = _dapper.GetAll<ApprovalSettingDTO>(ApplicationConstants.Sp_ApprovalSettings, param, commandType: CommandType.StoredProcedure);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message, "An error has occured");
        //        return new List<ApprovalSettingDTO>();
        //    }
        //}
        public async Task<List<ApprovalSettingDTO>> GetApprovalSettings()
        {
            try
            {
                var response = await _context.ApprovalSetting
                        .Include(c => c.MenuSetup)
                        .Where(c => c.MerchantCode == _authenticatedUser.MerchantCode)
                        .Select(c => new ApprovalSettingDTO
                        {
                            ApprovalSettingId = c.ApprovalSettingId,
                            NoOfRequiredApproval = c.NoOfRequiredApproval,
                            MerchantCode = c.MerchantCode,
                            CreatedOn = Convert.ToDateTime(c.CreatedOn).ToString("dd/MM/yyyy hh:mm tt"),
                            MenuName = c.MenuSetup.MenuName,
                            MenuSetupId = c.MenuSetupId
                        }).ToListAsync();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return new List<ApprovalSettingDTO>();
            }
        }

        public RepositoryResponse UpdateApprovalSetting(UpdateApprovalSettingDTO request)
        {
            try
            {

                var param = new DynamicParameters();
                param.Add("Status", Status.UPDATE);
                param.Add("MerchantCode", _authenticatedUser.MerchantCode);
                param.Add("ApprovalSettingId", request.ApprovalSettingId);
                param.Add("NoOfRequiredApproval", request.NoOfRequiredApproval);
                param.Add("UpdatedBy", _authenticatedUser.UserId);
                var response = _dapper.Execute(ApplicationConstants.Sp_ApprovalSettings, param, CommandType.StoredProcedure);
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

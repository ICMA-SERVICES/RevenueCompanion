using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.Account;
using RevenueCompanion.Application.DTOs.Merchant;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly ILogger<MerchantRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public MerchantRepository(ILogger<MerchantRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IDapper dapper,
                               ApplicationDbContext context,
                               IAccountService accountService)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _dapper = dapper;
            _context = context;
            _accountService = accountService;
            constring = myconnectionString.Value.DefaultConnection;
        }
        public async Task<RepositoryResponse> CreateMerchantAsync(CreateMerchantDTO request)
        {
            try
            {
                if (await _context.MerchantConfig.AnyAsync(c => c.MerchantCode == request.MerchantCode))
                    return ApplicationConstants.RepositoryExists();

                var merchant = new MerchantConfig
                {
                    Name = request.Name,
                    BaseUrl = request.BaseUrl,
                    BgImage = request.BgImage,
                    Email = request.Email,
                    Logo = request.Logo,
                    MerchantCode = request.MerchantCode,
                    Phone = request.Phone
                };
                await _context.AddAsync(merchant);
                var response = await _context.SaveChangesAsync();

                var result = await _accountService.RegisterAsync(new RegisterRequest
                {
                    Email = request.Email,
                    FirstName = request.Name,
                    MerchantCode = request.MerchantCode,
                    RoleId = "MerchantAdmin",
                    IsWithRoleName = true,
                    UserName = request.Email,
                    WebUrl = request.WebUrl
                });
                if (!result.Succeeded)
                {
                    _context.MerchantConfig.Remove(merchant);
                    await _context.SaveChangesAsync();
                    return ApplicationConstants.RepositoryFailed();
                }
                return ApplicationConstants.RepositorySuccess();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured");
                return ApplicationConstants.RepositoryFailed();
            }
        }

        public async Task<MerchantDTO> GetFirstMerchant()
        {
            try
            {
                var merchant = await _context.MerchantConfig.FirstOrDefaultAsync();
                if (merchant is null)
                    return null;
                return new MerchantDTO
                {
                    BaseUrl = merchant.BaseUrl,
                    BgImage = merchant.BgImage,
                    Email = merchant.Email,
                    Logo = merchant.Logo,
                    MerchantCode = merchant.MerchantCode,
                    MerchantId = merchant.MerchantConfigId,
                    Name = merchant.Name,
                    Phone = merchant.Phone
                };
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, "An error has occured");
                return null;
            }
        }
    }
}

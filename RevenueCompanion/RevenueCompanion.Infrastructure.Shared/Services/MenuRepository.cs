using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RevenueCompanion.Application.Constants;
using RevenueCompanion.Application.DapperServices;
using RevenueCompanion.Application.DTOs;
using RevenueCompanion.Application.DTOs.MenuSetup;
using RevenueCompanion.Application.Enums;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Shared.Services
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ILogger<MenuRepository> _logger;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private string constring;
        IOptions<ConnectionStrings> myconnectionString;
        private readonly IDapper _dapper;
        private readonly ApplicationDbContext _context;
        private readonly IAuditRepository _auditRepository;

        public MenuRepository(ILogger<MenuRepository> logger,
                               IAuthenticatedUserService authenticatedUser,
                               IOptions<ConnectionStrings> connectionString,
                               IDapper dapper,
                               ApplicationDbContext context,
                               IAuditRepository auditRepository)
        {
            _logger = logger;
            _authenticatedUser = authenticatedUser;
            myconnectionString = connectionString;
            _dapper = dapper;
            _context = context;
            _auditRepository = auditRepository;
            constring = myconnectionString.Value.DefaultConnection;
        }
        public RepositoryResponse CreateMenuSetup(CreateMenuSetupDTO createMenuDTO)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("Status", Status.INSERT);
                param.Add("MenuId", createMenuDTO.MenuId);
                param.Add("MenuUrl", createMenuDTO.MenuUrl);
                param.Add("RoleName", createMenuDTO.RoleName);
                param.Add("MenuName", createMenuDTO.MenuName);
                param.Add("IconClass", createMenuDTO.IconClass);
                param.Add("ParentMenuId", createMenuDTO.ParentMenuId);
                param.Add("IconClass", createMenuDTO.IconClass);
                param.Add("CreatedBy", _authenticatedUser.UserId);
                param.Add("RequiresApproval", createMenuDTO.RequiresApproval);

                var response = _dapper.Insert<int>(ApplicationConstants.Sp_MenuSetup, param, CommandType.StoredProcedure);
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

        public async Task<List<MenuSetupDTO>> GetMenu(string RoleID)
        {
            try
            {
                int startCount = 0;
                var response = await _context.MenuSetup.Where(c => (c.ParentMenuId == null || c.ParentMenuId != "*") && c.RoleId == RoleID).Select(c => new MenuSetupDTO
                {
                    RoleId = c.RoleId,
                    ParentMenuId = c.ParentMenuId,
                    MenuId = c.MenuId,
                    MenuName = c.MenuName,
                    MenuUrl = c.MenuUrl,
                    IsActive = c.IsActive,
                    RoleName = c.RoleName

                }).ToListAsync();
                response.ForEach(c =>
                {
                    c.MenuSetupId = startCount++;
                });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error occured while getting a Menu");
                return new List<MenuSetupDTO>();
            }
        }
        public async Task<MerchantConfig> GetMerchantSetup(string url)
        {
            MerchantConfig result = new MerchantConfig();
            try
            {
                result = await _context.MerchantConfig.FirstOrDefaultAsync(x => x.BaseUrl.ToLower().Equals(url));

                return result;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public async Task<List<MenuSetupDTO>> GetPagesAssignedToUser(string UserID, string userPageSecret)
        {
            try
            {
                if (userPageSecret != null && userPageSecret != "")
                {
                    var result = await _context.UsersRolePermission
                                 .Include(c => c.MenuSetup)
                                 .Where(c => c.UserId == UserID && c.IsActive == true && c.IsDeleted == false)
                                 .Select(c => new MenuSetupDTO
                                 {
                                     IconClass = c.MenuSetup.IconClass,
                                     IsActive = c.IsActive,
                                     MenuId = c.MenuSetup.MenuId,
                                     MenuName = c.MenuSetup.MenuName,
                                     MenuSetupId = c.MenuSetupId,
                                     MenuUrl = c.MenuSetup.MenuUrl,
                                     UserId = c.UserId,
                                     RoleId = c.MenuSetup.RoleId
                                 }).ToListAsync();
                    return result;
                }
                var response = new List<MenuSetupDTO>();
                var activePagesAssignedToUser = await _context.UsersRolePermission.Where(c => c.UserId == UserID && c.IsActive == true && c.IsDeleted == false).ToListAsync();
                foreach (var item in activePagesAssignedToUser)
                {
                    var menuDetail = await _context.MenuSetup.FirstOrDefaultAsync(c => c.MenuSetupId == item.MenuSetupId);
                    if (menuDetail.ParentMenuId != null)
                    {
                        var parentMenu = await _context.MenuSetup.FirstOrDefaultAsync(c => c.MenuId == menuDetail.ParentMenuId);
                        if (!response.Any(c => c.MenuId == parentMenu.MenuId))
                        {
                            //include the parent
                            response.Add(new MenuSetupDTO
                            {
                                MenuSetupId = parentMenu.MenuSetupId,
                                MenuName = parentMenu.MenuName,
                                MenuId = parentMenu.MenuId,
                                MenuUrl = parentMenu.MenuUrl,
                                ParentMenuId = parentMenu.ParentMenuId,
                                UserId = UserID,
                                IconClass = parentMenu.IconClass,
                            });
                        }
                        response.Add(new MenuSetupDTO
                        {
                            MenuSetupId = menuDetail.MenuSetupId,
                            MenuName = menuDetail.MenuName,
                            MenuId = menuDetail.MenuId,
                            MenuUrl = menuDetail.MenuUrl,
                            ParentMenuId = menuDetail.ParentMenuId,
                            UserId = UserID,
                            IconClass = menuDetail.IconClass
                        });
                    }
                    else
                    {
                        response.Add(new MenuSetupDTO
                        {
                            MenuSetupId = menuDetail.MenuSetupId,
                            MenuName = menuDetail.MenuName,
                            MenuId = menuDetail.MenuId,
                            MenuUrl = menuDetail.MenuUrl,
                            ParentMenuId = menuDetail.ParentMenuId,
                            UserId = UserID,
                            IconClass = menuDetail.IconClass
                        });
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while creating menu");
                return new List<MenuSetupDTO>();
            }
        }



        public async Task<int> AssignMenusToUser(MenuToUserRequest model)
        {
            try
            {
                var counter = 0;
                foreach (var menu in model.Menus)
                {
                    var menuIdentity = await _context.MenuSetup.FirstOrDefaultAsync(c => c.MenuId == menu);
                    var IsMenuAssignedPreviously = await _context.UsersRolePermission.AnyAsync(c => c.MenuSetupId == menuIdentity.MenuSetupId && c.UserId == model.UserId && c.IsDeleted == false);
                    if (!IsMenuAssignedPreviously)
                    {
                        var userRolePermission = new UsersRolePermission
                        {
                            MenuSetupId = menuIdentity.MenuSetupId,
                            UserId = model.UserId,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = model.UserId
                        };
                        await _context.UsersRolePermission.AddAsync(userRolePermission);
                        var result = await _context.SaveChangesAsync();
                        if (result > 0)
                        {
                            string action = $"Assigned {menuIdentity.MenuName} to {model.UserId}";
                            await _auditRepository.CreateAudit(model.UserId, action);
                        }
                        counter++;
                    }
                    else
                    {
                        counter = -1;
                    }
                }

                return counter;

            }
            catch (Exception ex)
            {

                return 0; ;
            }
        }
        public async Task<int> DeleteMenuAssignedToUser(MenuToUserRequest model)
        {
            try
            {
                var assignedMenus = await _context.UsersRolePermission
                        .Where(c => c.IsActive && c.IsDeleted == false && model.Menus
                        .Contains(c.MenuSetup.MenuId))
                        .ToListAsync();
                _context.UsersRolePermission.RemoveRange(assignedMenus);
                var response = await _context.SaveChangesAsync();
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting menu assign to users");
                return 0;
            }
        }

        public async Task<List<MenuSetupDTO>> GetMenusWithApprovalSettingAsTrue()
        {
            var response = await _context.MenuSetup.Where(c => c.ParentMenuId != "*" && c.RequiresApproval).Select(c => new MenuSetupDTO
            {
                RoleId = c.RoleId,
                ParentMenuId = c.ParentMenuId,
                MenuId = c.MenuId,
                MenuName = c.MenuName,
                MenuUrl = c.MenuUrl,
                IsActive = c.IsActive,
                RoleName = c.RoleName,
                MenuSetupId = c.MenuSetupId

            }).ToListAsync();
            return response;
        }
    }
}

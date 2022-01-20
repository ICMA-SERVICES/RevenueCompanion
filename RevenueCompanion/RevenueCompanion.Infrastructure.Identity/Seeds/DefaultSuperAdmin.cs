using Microsoft.AspNetCore.Identity;
using RevenueCompanion.Domain.Entities;
using RevenueCompanion.Infrastructure.Identity.Models;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Icmaapp_@development247",
                Email = "Icmaapp_@development247",
                FirstName = "Admin",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Icmaapp_@development247");
                    await userManager.AddToRoleAsync(defaultUser, "GlobalAdmin");

                    foreach (var menuId in GlobalAdminPages)
                    {
                        var menuSetupId = context.MenuSetup.FirstOrDefault(c => c.MenuId == menuId).MenuSetupId;
                        var defaultPage = new UsersRolePermission
                        {
                            MenuSetupId = menuSetupId,
                            MerchantCode = "",
                            UserId = user.Id,
                            IsActive = true,
                            CreatedOn = DateTime.Now,
                            CreatedBy = defaultUser.Id,
                        };
                        await context.UsersRolePermission.AddAsync(defaultPage);
                        await context.SaveChangesAsync();


                    }
                }

            }
        }
        private static List<string> GlobalAdminPages = new List<string> { "GlobalDashboard", "ApprovalSettings", "Users" };
    }
}

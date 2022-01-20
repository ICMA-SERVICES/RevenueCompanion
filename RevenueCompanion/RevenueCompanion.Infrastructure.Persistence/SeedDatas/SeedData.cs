using Microsoft.EntityFrameworkCore;
using RevenueCompanion.Domain.Entities;
using System;

namespace RevenueCompanion.Infrastructure.Persistence.SeedDatas
{
    public static class SeedData
    {

        public static void SeedDefaultPageData(this ModelBuilder builder)
        {
            builder.Entity<MenuSetup>().HasData(
                new MenuSetup[]
                {
                    new MenuSetup{ CreatedBy = "", CreatedOn = DateTime.Now,  IconClass = "ion-home", IsActive = true, MenuId = "UserDashobard", MenuName = "Dashboard", ParentMenuId = null, MerchantCode = "", MenuUrl = "/Dashboard/User", RequiresApproval = false, MenuSetupId = 1014}

                }
              );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RevenueCompanion.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<MenuSetup> MenuSetup { get; set; }
        public DbSet<CreditNoteRequestApprovalDetail> CreditNoteRequestApprovalDetails { get; set; }
        public DbSet<ApprovalSetting> ApprovalSetting { get; set; }
        public DbSet<CreditNoteRequest> CreditNoteRequest { get; set; }
        public DbSet<CreditNoteRequestType> CreditNoteRequestType { get; set; }
        public DbSet<App> App { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<MerchantConfig> MerchantConfig { get; set; }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<UsersRolePermission> UsersRolePermission { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = _dateTime.NowUtc;
                        entry.Entity.UpdatedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            // builder.SeedDefaultPageData();
            base.OnModelCreating(builder);
        }

    }
}

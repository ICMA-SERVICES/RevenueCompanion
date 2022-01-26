using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Application.Interfaces.Repositories;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Domain.Settings;
using RevenueCompanion.Infrastructure.Shared.Services;

namespace RevenueCompanion.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.Configure<MailMessageSettings>(_config.GetSection("MailMessageSettings"));
            services.Configure<ExternalLinks>(_config.GetSection("ExternalLinks"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAppRepository, AppRepository>();
            services.AddTransient<IApprovalSettingRepository, ApprovalSettingRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddTransient<IMerchantRepository, MerchantRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IAuditRepository, AuditRepository>();
            services.AddTransient<ICreditNoteRepository, CreditNoteRepository>();
            services.AddTransient<ISummaryReportRepository, SummaryReportRepository>();
            services.AddTransient<IHttpClientHelperService, HttpClientHelperService>();
        }
    }
}

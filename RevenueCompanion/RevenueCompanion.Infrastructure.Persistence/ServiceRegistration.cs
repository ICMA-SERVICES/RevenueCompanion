using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Domain.Common;
using RevenueCompanion.Infrastructure.Persistence.Contexts;
using RevenueCompanion.Infrastructure.Persistence.Repository;

namespace RevenueCompanion.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

                services.AddDbContext<AssessmentsContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("AssessmentConnection"),
                   b => b.MigrationsAssembly(typeof(AssessmentsContext).Assembly.FullName)));

                services.AddDbContext<IReconcileContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("IReconcileContextConnection"),
                   b => b.MigrationsAssembly(typeof(IReconcileContext).Assembly.FullName)));

                services.AddDbContext<IcmaCollectionContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("IcmaCollectionConnection"),
                   b => b.MigrationsAssembly(typeof(IcmaCollectionContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            //services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            #endregion
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();
            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //    try
            //    {
            //        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //        var dbContext = services.GetRequiredService<ApplicationDbContext>();
            //        await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
            //        await Infrastructure.Identity.Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager, dbContext);

            //        Log.Information("Finished Seeding Default Data");
            //        Log.Information("Application Starting");
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Warning(ex, "An error occurred seeding the DB");
            //    }
            //    finally
            //    {
            //        Log.CloseAndFlush();
            //    }
            //}
            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            var configSettings = new ConfigurationBuilder()
                               .AddJsonFile("appsettings.json")
                               .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configSettings)
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(config =>
                    {
                        config.AddConfiguration(configSettings);
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.AddSerilog();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}

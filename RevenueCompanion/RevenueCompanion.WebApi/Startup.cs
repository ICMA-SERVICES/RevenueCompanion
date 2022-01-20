using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RevenueCompanion.Application;
using RevenueCompanion.Application.Interfaces;
using RevenueCompanion.Infrastructure.Identity;
using RevenueCompanion.Infrastructure.Identity.Models;
using RevenueCompanion.Infrastructure.Persistence;
using RevenueCompanion.Infrastructure.Shared;
using RevenueCompanion.WebApi.Extensions;
using RevenueCompanion.WebApi.Services;
using System;
using System.Threading.Tasks;

namespace RevenueCompanion.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);
            services.AddSwaggerExtension();
            services
               .AddCors(options =>
               {
                   options.AddPolicy("CorsPolicy", builder =>
                   {
                       builder.AllowAnyOrigin();
                       builder.AllowAnyMethod();
                       builder.AllowAnyHeader();
                   });
               });

            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            CreateRolesAndSuperUser(serviceProvider).Wait();
            app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }
        private async Task CreateRolesAndSuperUser(IServiceProvider serviceProvider)
        {
            try
            {
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string[] roleNames = { "GlobalAdmin", "Approver", "Maker" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1
                        roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                //Here you could create a super user who will maintain the web app
                var poweruser = new ApplicationUser
                {

                    UserName = _config.GetSection("UserSettings")["UserEmail"],
                    Email = _config.GetSection("UserSettings")["UserEmail"],
                    MerchantCode = _config.GetSection("UserSettings")["MerchantCode"],
                    IsActive = true,
                    EmailConfirmed = true
                };
                //Ensure you have these values in your appsettings.json file
                string userPWD = _config.GetSection("UserSettings")["UserPassword"];
                var _user = await UserManager.FindByEmailAsync(_config.GetSection("UserSettings")["UserEmail"]);

                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                    if (createPowerUser.Succeeded)
                    {
                        //here we tie the new user to the role
                        await UserManager.AddToRoleAsync(poweruser, "GlobalAdmin");

                    }
                }


            }
            catch (Exception ex)
            {
                // error occured
            }
            //initializing custom roles 

        }

    }
}

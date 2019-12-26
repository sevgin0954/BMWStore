using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BMWStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            SeedInitialData(webHost);
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void SeedInitialData(IWebHost webHost)
        {
            using (var serviceScope = webHost.Services.CreateScope())
            {
                var serviceProvider = serviceScope.ServiceProvider;

                try
                {
                    var seedDbRolesService = serviceProvider.GetRequiredService<ISeedDbRolesService>();
                    var seedDbUsersService = serviceProvider.GetRequiredService<ISeedDbUsersService>();
                    var seedDbStatusesService = serviceProvider.GetRequiredService<ISeedDbStatusesService>();

                    seedDbRolesService
                        .SeedRolesAsync(WebConstants.AdminRoleName, WebConstants.UserRoleName, WebConstants.SupportRoleName)
                        .GetAwaiter()
                        .GetResult();

                    var userModel = new UserServiceModel()
                    {
                        Email = IdentityConstants.AdminEmail,
                        FirstName = IdentityConstants.AdminEmail,
                        LastName = IdentityConstants.AdminEmail,
                        UserName = IdentityConstants.AdminEmail
                    };
                    seedDbUsersService
                        .SeedUserAsync(userModel, IdentityConstants.AdminPassword, WebConstants.AdminRoleName)
                        .GetAwaiter()
                        .GetResult();

                    seedDbStatusesService
                        .SeedTestDriveStatusesAsync(Enum.GetNames(typeof(TestDriveStatus)))
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, e.Message);
                }
            }
        }
    }
}

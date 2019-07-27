using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Services.Interfaces;
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
                    var seedDbService = serviceProvider.GetRequiredService<ISeedDbService>();

                    seedDbService
                        .SeedRolesAsync(WebConstants.AdminRoleName, WebConstants.UserRoleName, WebConstants.SupportRoleName)
                        .GetAwaiter()
                        .GetResult();
                    seedDbService
                        .SeedAdminAsync(IdentityConstants.AdminPassword, IdentityConstants.AdminEmail)
                        .GetAwaiter()
                        .GetResult();
                    seedDbService
                        .SeedTestDriveStatuses(Enum.GetNames(typeof(TestDriveStatus)))
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred.");
                }
            }
        }
    }
}

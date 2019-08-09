using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.SeedDbRolesServiceTests
{
    public class SeedRolesAsyncTests : BaseSeedDbRolesServiceTest
    {
        [Fact]
        public async void WithStatuses_ShouldAddOnlyNotExistingStatuses()
        {
            var dbContext = this.GetDbContext();
            var dbRole = SeedRolesMethods.SeedUserRole(dbContext);
            var newRoleName = Guid.NewGuid().ToString();
            var service = this.GetService(dbContext);

            await service.SeedRolesAsync(dbRole.Name, newRoleName);

            Assert.Equal(2, dbContext.Roles.Count());
            Assert.Equal(dbRole, dbContext.Roles.First());
        }

        [Fact]
        public async void WithAlreadyExistingStatus_ShouldDoNothing()
        {
            var dbContext = this.GetDbContext();
            var dbRole = SeedRolesMethods.SeedUserRole(dbContext);
            var service = this.GetService(dbContext);

            await service.SeedRolesAsync(dbRole.Name);

            Assert.Single(dbContext.Roles);
            Assert.Equal(dbRole, dbContext.Roles.First());
        }
    }
}

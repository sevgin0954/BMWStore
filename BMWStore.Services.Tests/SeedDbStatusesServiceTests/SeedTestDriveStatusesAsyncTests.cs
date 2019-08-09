using BMWStore.Common.Enums;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.SeedDbStatusesServiceTests
{
    public class SeedTestDriveStatusesAsyncTests : BaseSeedDbStatusesServiceTest
    {
        [Fact]
        public async void WithStatuses_ShouldAddOnlyNotExistingStatuses()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Canceled);
            var upcomingStatusName = TestDriveStatus.Upcoming.ToString();
            var service = this.GetService(dbContext);

            await service.SeedTestDriveStatusesAsync(dbStatus.Name, upcomingStatusName);

            Assert.Equal(2, dbContext.Statuses.Count());
            Assert.Contains(dbContext.Statuses, s => s.Name == dbStatus.Name);
            Assert.Contains(dbContext.Statuses, s => s.Name == upcomingStatusName);
        }

        [Fact]
        public async void WithAlreadyExistingStatus_ShouldDoNothing()
        {
            var dbContext = this.GetDbContext();
            var dbStatus = SeedStatusesMethods.SeedStatus(dbContext, TestDriveStatus.Canceled);
            var service = this.GetService(dbContext);

            await service.SeedTestDriveStatusesAsync(dbStatus.Name);

            Assert.Equal(1, dbContext.Statuses.Count());
            Assert.Contains(dbContext.Statuses, s => s.Name == dbStatus.Name);
        }
    }
}

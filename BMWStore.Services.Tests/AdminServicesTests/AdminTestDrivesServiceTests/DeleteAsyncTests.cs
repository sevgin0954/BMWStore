using BMWStore.Common.Constants;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class DeleteAsyncTests : BaseAdminTestDrivesServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldDeleteTestDrive()
        {
            var dbContext = this.GetDbContext();
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithStatus(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbTestDrive.Id);

            Assert.Empty(dbContext.TestDrives);
        }
    }
}

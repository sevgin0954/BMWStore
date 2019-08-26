using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class ChangeTestDriveStatusToPassedAsyncTests : BaseAdminTestDrivesServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(
                async () => await service.ChangeTestDriveStatusToPassedAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithotPassedStatus_ShouldTrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithStatus(dbContext, TestDriveStatus.Canceled);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await service.ChangeTestDriveStatusToPassedAsync(dbTestDrive.Id));
        }

        [Fact]
        public async void WithPassedTestDrive_ShouldTrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDrive(dbContext, "", this.PassedStatus);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await service.ChangeTestDriveStatusToPassedAsync(dbTestDrive.Id));
            Assert.Equal(ErrorConstants.StatusIsNotUpcoming, exception.Message);
        }

        [Fact]
        public async void WithCanceledTestDrive_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDriveWithStatus(dbContext, TestDriveStatus.Canceled);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await service.ChangeTestDriveStatusToPassedAsync(dbTestDrive.Id));
            Assert.Equal(ErrorConstants.StatusIsNotUpcoming, exception.Message);
        }

        [Fact]
        public async void WithUpcomingTestDrive_ShouldChangeStatusToPassed()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTestDrive = SeedTestDrivesMethods.SeedTestDrive(dbContext, "", this.UpcomingStatus);

            await service.ChangeTestDriveStatusToPassedAsync(dbTestDrive.Id);

            Assert.Equal(TestDriveStatus.Passed.ToString(), dbTestDrive.Status.Name);
        }
    }
}
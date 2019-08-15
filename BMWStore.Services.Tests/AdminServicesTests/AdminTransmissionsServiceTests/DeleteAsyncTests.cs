using BMWStore.Common.Constants;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class DeleteAsyncTests : BaseAdminTransmissionsServiceTests
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
        public async void WithCorrectId_ShouldDeleteCar()
        {
            var dbContext = this.GetDbContext();
            var dbTransmission = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbTransmission.Id);

            Assert.Empty(dbContext.Transmissions);
        }
    }
}

using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class GetByIdAsyncTests : BaseAdminTransmissionsServiceTests
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetByIdAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnModel()
        {
            var dbContext = this.GetDbContext();
            var dbTransmission = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetByIdAsync(dbTransmission.Id);

            Assert.NotNull(model);
        }
    }
}

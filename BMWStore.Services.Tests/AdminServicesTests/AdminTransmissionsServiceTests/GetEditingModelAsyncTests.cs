using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class GetEditingModelAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetEditingModelAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnTransmissionWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbTransmission = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbTransmission.Id);

            Assert.Equal(dbTransmission.Name, model.Name);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnTransmissionWithCorrectPrice()
        {
            var dbContext = this.GetDbContext();
            var dbTransmission = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbTransmission.Id);

            Assert.Equal(dbTransmission.Price, model.Price);
        }
    }
}

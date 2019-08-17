using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class GetEditingModelAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
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
        public async void WithCorrectId_ShouldReturnModelWthCorrectId()
        {
            var dbContext = this.GetDbContext();
            var dbFuelType = SeedFuelTypesMethods.SeedFuelType(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbFuelType.Id);

            Assert.Equal(dbFuelType.Id, model.Id);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnModelWthCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbFuelType = SeedFuelTypesMethods.SeedFuelType(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbFuelType.Id);

            Assert.Equal(dbFuelType.Name, model.Name);
        }
    }
}

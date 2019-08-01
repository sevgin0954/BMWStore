using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class DeleteAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public DeleteAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldDeleteFuelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var fuelType = this.SeedFuelType(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(fuelType.Id);

            Assert.Empty(dbContext.FuelTypes);
        }
    }
}

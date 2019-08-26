using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class DeleteAsyncTests : BaseAdminCarsServiceTest
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
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbCar.Id);

            Assert.Empty(dbContext.BaseCars);
        }
    }
}

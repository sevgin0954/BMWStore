using BMWStore.Common.Constants;
using BMWStore.Entities;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class DeleteCarAsyncTests : BaseAdminCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public DeleteCarAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithCorrectIdForNewCar_ShouldDeleteCar()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbNewCar = CommonSeedTestMethods.SeedCar<NewCar>(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteCarAsync(dbNewCar.Id);

            Assert.Empty(dbContext.BaseCars.ToList());
        }

        [Fact]
        public async void WithCorrectIdForUsedCar_ShouldDeleteCar()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbUsedCar = CommonSeedTestMethods.SeedCar<UsedCar>(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteCarAsync(dbUsedCar.Id);

            Assert.Empty(dbContext.BaseCars.ToList());
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var incorrectId = Guid.NewGuid().ToString();
            var service = this.GetService(dbContext);

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteCarAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }
    }
}

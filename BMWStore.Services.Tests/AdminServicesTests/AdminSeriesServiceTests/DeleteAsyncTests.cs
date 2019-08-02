using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class DeleteAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<BaseTestFixture>
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
        public async void WithCorrectId_ShouldDeleteSeries()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbSeries = this.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbSeries.Id);

            Assert.Empty(dbContext.FuelTypes);
        }
    }
}

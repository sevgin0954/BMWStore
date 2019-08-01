using BMWStore.Common.Constants;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class DeleteAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public DeleteAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldTrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.DeleteAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldDeleteEngine()
        {
            var dbContext = this.baseTest.GetDbContext();
            var engineId = Guid.NewGuid().ToString();
            this.SeedEngine(engineId, dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(engineId);

            Assert.Empty(dbContext.Engines);
        }
    }
}

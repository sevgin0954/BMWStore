using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDeleteServiceTests
{
    public class DeleteAsyncTests : BaseAdminDeleteServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.DeleteAsync<Engine>(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldDeleteEntity()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbEngine = SeedEnginesMethods.SeedEngine(dbContext);

            await service.DeleteAsync<Engine>(dbEngine.Id);

            Assert.Empty(dbContext.Engines);
        }
    }
}

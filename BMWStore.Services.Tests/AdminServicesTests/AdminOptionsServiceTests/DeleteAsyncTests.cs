using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class DeleteAsyncTests : BaseAdminOptionsServiceTest
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
        public async void WithCorrectId_ShouldDeleteOption()
        {
            var dbContext = this.GetDbContext();
            var dbOption = SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbOption.Id);

            Assert.Empty(dbContext.Options);
        }
    }
}

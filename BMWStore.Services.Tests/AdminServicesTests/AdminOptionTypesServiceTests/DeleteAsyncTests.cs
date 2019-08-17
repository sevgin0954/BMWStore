using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class DeleteAsyncTests : BaseAdminOptionTypesServiceTest
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
        public async void WithCorrectId_ShouldDeleteModelType()
        {
            var dbContext = this.GetDbContext();
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var service = this.GetService(dbContext);

            await service.DeleteAsync(dbOptionType.Id);

            Assert.Empty(dbContext.OptionTypes);
        }
    }
}

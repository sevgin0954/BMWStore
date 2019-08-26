using BMWStore.Common.Constants;
using BMWStore.Models.OptionModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetByIdAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await service.GetByIdAsync<OptionViewModel>(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnOption()
        {
            var dbContext = this.GetDbContext();
            var dbOption = SeedOptionsMethods.SeedOptionWithOptionType(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetByIdAsync<OptionViewModel>(dbOption.Id);

            Assert.NotNull(model);
        }
    }
}

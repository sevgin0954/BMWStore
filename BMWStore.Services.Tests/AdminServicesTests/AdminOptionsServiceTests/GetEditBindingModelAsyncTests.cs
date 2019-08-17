using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetEditBindingModelAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.GetEditBindingModelAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnModel()
        {
            var dbContext = this.GetDbContext();
            var dbOption = SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditBindingModelAsync(dbOption.Id);

            Assert.Equal(dbOption.Id, model.Id);
        }
    }
}

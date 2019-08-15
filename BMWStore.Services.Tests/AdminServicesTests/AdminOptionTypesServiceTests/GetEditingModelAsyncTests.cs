using BMWStore.Common.Constants;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class GetEditingModelAsyncTests : BaseAdminOptionTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetEditingModelAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_SgouldReturnModelWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbOptionType.Id);

            Assert.Equal(dbOptionType.Name, model.Name);
        }
    }
}

using BMWStore.Common.Constants;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class GetEditingModelAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<MapperFixture>
    {
        // TODO: Code repeat
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.GetEditingModelAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnModelWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbSeries = SeedSeriesMethods.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditingModelAsync(dbSeries.Id);

            Assert.Equal(dbSeries.Name, model.Name);
        }
    }
}

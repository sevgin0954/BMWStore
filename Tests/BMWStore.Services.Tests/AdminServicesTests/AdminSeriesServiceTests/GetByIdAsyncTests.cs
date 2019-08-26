using BMWStore.Common.Constants;
using BMWStore.Models.SeriesModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class GetByIdAsyncTests : BaseAdminSeriesServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetByIdAsync<SeriesViewModel>(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnSeries()
        {
            var dbContext = this.GetDbContext();
            var dbSeries = SeedSeriesMethods.SeedSeries(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetByIdAsync<SeriesViewModel>(dbSeries.Id);

            Assert.NotNull(model);
        }
    }
}

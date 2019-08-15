using BMWStore.Models.SeriesModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class CreateNewSeriesAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewSeries()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new SeriesCreateBindingModel();

            await service.CreateNewSeriesAsync(model);

            Assert.Single(dbContext.Series);
        }
    }
}

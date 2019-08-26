using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class CreateNewAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewSeries()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new SeriesServiceModel();

            await service.CreateNewAsync(model);

            Assert.Single(dbContext.Series);
        }
    }
}

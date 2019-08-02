using BMWStore.Models.SeriesModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminSeriesServiceTests
{
    public class CreateNewSeriesAsyncTests : BaseAdminSeriesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateNewSeriesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewSeries()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminSeriesCreateBindingModel();

            await service.CreateNewSeriesAsync(model);

            Assert.Single(dbContext.Series);
        }
    }
}

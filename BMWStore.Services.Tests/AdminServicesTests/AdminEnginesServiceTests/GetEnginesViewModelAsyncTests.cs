using BMWStore.Data;
using BMWStore.Data.SortStrategies.EngineStrategies.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class GetEnginesViewModelAsync : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutEngines_ShouldReturnModelWithEmptyEnginesCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await this.CallGetEnginesViewModelAsync(dbContext);

            Assert.Empty(model.Engines);
        }

        [Fact]
        public async void WithEngines_ShouldReturnModelWithEngines()
        {
            var dbContext = this.GetDbContext();
            SeedEnginesMethods.SeedEngineWithTransmission(dbContext);

            var model = await this.CallGetEnginesViewModelAsync(dbContext);

            Assert.Single(model.Engines);
        }

        [Fact]
        public async void WithPageNumber_ShouldReturnCorrectCurrentPage()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetEnginesViewModelAsync(dbContext);

            Assert.Equal(1, model.CurrentPage);
        }

        [Fact]
        public async void WithoutEngines_ShouldReturnZeroTotalPagesCount()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetEnginesViewModelAsync(dbContext);

            Assert.Equal(0, model.TotalPagesCount);
        }

        [Fact]
        public async void WithEngine_ShouldReturnCorrectTotalPagesCount()
        {
            var dbContext = this.GetDbContext();
            SeedEnginesMethods.SeedEngineWithTransmission(dbContext);

            var model = await this.CallGetEnginesViewModelAsync(dbContext);

            Assert.Equal(1, model.TotalPagesCount);
        }

        private async Task<AdminEnginesViewModel> CallGetEnginesViewModelAsync(
            ApplicationDbContext dbContext, 
            int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var sortStrategy = GetSortStrategy(dbContext.Engines);
            var models = await service.GetEnginesViewModelAsync(pageNumber, sortStrategy);

            return models;
        }

        private IEngineSortStrategy GetSortStrategy(IQueryable<Engine> engines)
        {
            var mockedSortStrategy = new Mock<IEngineSortStrategy>();
            mockedSortStrategy.Setup(ss => ss.Sort(engines))
                .Returns(engines);

            return mockedSortStrategy.Object;
        }
    }
}

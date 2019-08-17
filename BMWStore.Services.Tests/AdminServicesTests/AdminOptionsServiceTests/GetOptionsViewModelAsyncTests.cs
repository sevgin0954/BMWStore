using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data;
using BMWStore.Data.FilterStrategies.OptionStrategies;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetOptionsViewModelAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutOptions_ShouldReturnModelWithEmptyOptionsCollection()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetOptionsViewModelAsync(dbContext);

            Assert.Empty(model.Options);
        }

        [Fact]
        public async void WithBiggerPageNumber_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedOptionsMethods.SeedOptionWithOptionType(dbContext);

            var model = await this.CallGetOptionsViewModelAsync(dbContext, 2);

            Assert.Empty(model.Options);
        }

        [Fact]
        public async void WithOption_ShouldReturnModelWithCorrectTotalPagesCount()
        {
            var dbContext = this.GetDbContext();
            SeedOptionsMethods.SeedOptionWithOptionType(dbContext);

            var model = await this.CallGetOptionsViewModelAsync(dbContext);

            Assert.Equal(1, model.TotalPagesCount);
        }

        [Fact]
        public async void WithOption_ShouldReturnModelWithOption()
        {
            var dbContext = this.GetDbContext();
            SeedOptionsMethods.SeedOptionWithOptionType(dbContext);

            var model = await this.CallGetOptionsViewModelAsync(dbContext);

            Assert.Single(model.Options);
        }

        private async Task<AdminOptionsViewModel> CallGetOptionsViewModelAsync(
            ApplicationDbContext dbContext, 
            int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var filterStrategy = new ReturnAllFilterStrategy();
            var sortStrategy = OptionSortStrategyType.Name;
            var sortDirection = SortStrategyDirection.Ascending;
            var model = await service.GetOptionsViewModelAsync(filterStrategy, sortStrategy, sortDirection, pageNumber);

            return model;
        }
    }
}

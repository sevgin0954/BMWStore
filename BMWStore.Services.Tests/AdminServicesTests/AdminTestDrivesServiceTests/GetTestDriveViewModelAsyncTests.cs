using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data;
using BMWStore.Data.FilterStrategies.TestDrives;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class GetTestDriveViewModelAsyncTests : BaseAdminTestDrivesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyTestDriveCollection()
        {
            var dbContext = this.GetDbContext();

            var model = await this.CallGetTestDrivesViewModelAsync(dbContext);

            Assert.Empty(model.TestDrives);
        }

        [Fact]
        public async void WithTestDrive_ShouldReturnModelWithTestDrive()
        {
            var dbContext = this.GetDbContext();
            SeedTestDrivesMethods.SeedTestDriveWithEverything(dbContext);

            var model = await this.CallGetTestDrivesViewModelAsync(dbContext);

            Assert.Single(model.TestDrives);
        }

        private async Task<AdminTestDrivesViewModel> CallGetTestDrivesViewModelAsync(
            ApplicationDbContext dbContext, 
            int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var filterStrategy = new ReturnAllTestDrivesFilterStrategy();
            var sortType = AdminTestDrivesSortStrategyType.Date;
            var sortDirection = SortStrategyDirection.Ascending;
            var model = await service.GetTestDrivesViewModelAsync(filterStrategy, sortType, sortDirection, pageNumber);

            return model;
        }
    }
}

using BMWStore.Common.Enums;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class GetTestDriveViewModelAsyncTests : BaseAdminTestDrivesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyTestDriveCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var sortType = AdminTestDrivesSortStrategyType.Date;
            var sortDirect = SortStrategyDirection.Ascending;

            var model = await service.GetTestDriveViewModelAsync(sortType, sortDirect, 1);

            Assert.Empty(model.TestDrives);
        }

        [Fact]
        public async void WithTestDrive_ShouldReturnModelWithTestDrive()
        {
            var dbContext = this.GetDbContext();
            SeedTestDrivesMethods.SeedTestDriveWithEverything(dbContext);
            var service = this.GetService(dbContext);
            var sortType = AdminTestDrivesSortStrategyType.Date;
            var sortDirect = SortStrategyDirection.Ascending;

            var model = await service.GetTestDriveViewModelAsync(sortType, sortDirect, 1);

            Assert.Single(model.TestDrives);
        }
    }
}

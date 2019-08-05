using BMWStore.Common.Enums;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class GetTestDriveViewModelAsyncTests : BaseAdminTestDrivesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetTestDriveViewModelAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyTestDriveCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var sortType = AdminTestDrivesSortStrategyType.Date;
            var sortDirect = SortStrategyDirection.Ascending;

            var model = await service.GetTestDriveViewModelAsync(sortType, sortDirect, 1);

            Assert.Empty(model.TestDrives);
        }

        [Fact]
        public async void WithTestDrive_ShouldReturnModelWithTestDrive()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SeedTestDriveWithStatuses(dbContext);
            var service = this.GetService(dbContext);
            var sortType = AdminTestDrivesSortStrategyType.Date;
            var sortDirect = SortStrategyDirection.Ascending;

            var model = await service.GetTestDriveViewModelAsync(sortType, sortDirect, 1);

            Assert.Single(model.TestDrives);
        }
    }
}

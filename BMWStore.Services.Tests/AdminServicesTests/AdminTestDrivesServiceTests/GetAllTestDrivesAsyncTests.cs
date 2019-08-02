using BMWStore.Data.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class GetAllTestDrivesAsyncTests : BaseAdminTestDrivesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllTestDrivesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutTestDrives_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var sortStrategy = this.GetSortStrategy(dbContext.TestDrives.AsQueryable());

            var models = await service.GetAllTestDrivesAsync(sortStrategy);

            Assert.Empty(models);
        }

        [Fact]
        public async void WithTestDrive_ShouldReturnTestDrive()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SeedTestDriveWithStatuses(dbContext);
            var service = this.GetService(dbContext);
            var sortStrategy = this.GetSortStrategy(dbContext.TestDrives.AsQueryable());

            var models = await service.GetAllTestDrivesAsync(sortStrategy);

            Assert.Single(models);
        }

        private ITestDriveSortStrategy GetSortStrategy(IQueryable<TestDrive> testDrives)
        {
            var mockedSortStrategy = new Mock<ITestDriveSortStrategy>();
            mockedSortStrategy.Setup(ss => ss.Sort(testDrives))
                .Returns(testDrives);

            return mockedSortStrategy.Object;
        }
    }
}

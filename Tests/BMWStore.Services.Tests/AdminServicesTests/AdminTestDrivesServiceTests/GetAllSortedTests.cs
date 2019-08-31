using BMWStore.Data;
using BMWStore.Services.SortStrategies.TestDriveStrategies.Interfaces;
using BMWStore.Services.Models;
using BMWStore.Tests.Common.MockMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using Moq;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTestDrivesServiceTests
{
    public class GetAllSortedTests : BaseAdminTestDrivesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutTestDrives_ShouldReturnEmtyCollection()
        {
            var dbContext = this.GetDbContext();

            var models = this.CallGetAllSorted(dbContext);

            Assert.Empty(models);
        }

        [Fact]
        public void WithTestDrive_ShouldReturnTestDrive()
        {
            var dbContext = this.GetDbContext();
            SeedTestDrivesMethods.SeedTestDriveWithEverything(dbContext);

            var models = this.CallGetAllSorted(dbContext);

            Assert.Single(models);
        }

        [Fact]
        public void WithBiggerPageNumber_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            SeedTestDrivesMethods.SeedTestDriveWithEverything(dbContext);
            var pageNumber = 2;

            var models = this.CallGetAllSorted(dbContext, pageNumber);

            Assert.Empty(models);
        }

        private IQueryable<TestDriveServiceModel> CallGetAllSorted(ApplicationDbContext dbContext, int pageNumber = 1)
        {
            var service = this.GetService(dbContext);
            var mockedSortStrategy = new Mock<ITestDriveSortStrategy>();
            CommonMockMethods.SetupTestDriveSortStrategy(mockedSortStrategy);
            var models = service.GetAllSorted(dbContext.TestDrives, mockedSortStrategy.Object, pageNumber);

            return models;
        }
    }
}

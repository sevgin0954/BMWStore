using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminDashboardStatisticsServiceTests
{
    public class GetStatisticsAsyncTests : BaseAdminDashboardStatisticsServiceTest, IClassFixture<BaseTestFixture>
    {
        [Fact]
        public void WithoutRecords_ShouldReturnModelWithPropertiesWithValuesZero()
        {

        }

        [Fact]
        public void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalNewCarTestDrivesCount()
        {

        }

        [Fact]
        public void WithNewAndUsedCarTestDrives_ShouldReturnModelWithCorrectTotalUsedCarTestDrivesCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectNewCarsTestDrivesFromPast24HoursCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectUsedCarsTestDrivesFromPast24HoursCount()
        {

        }

        [Fact]
        public void WithTestDrives_ShouldReturnModelWithCorrectTestDrivesFromPast24HourCount()
        {

        }

        [Fact]
        public void WithUser_ShouldReturnModelWithCorrectTotalUsersCount()
        {

        }

        [Fact]
        public void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalNewCarsCount()
        {

        }

        [Fact]
        public void WithNewAndUsedCars_ShouldReturnModelWithCorrectTotalUsedCarsCount()
        {

        }
    }
}

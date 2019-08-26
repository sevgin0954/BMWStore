using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class GetAllAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutTransmissions_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Empty(models);
        }

        [Fact]
        public void WithTransmission_ShouldReturnTransmission()
        {
            var dbContext = this.GetDbContext();
            SeedTransmissionsMethods.SeedTransmission(dbContext);
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Single(models);
        }
    }
}
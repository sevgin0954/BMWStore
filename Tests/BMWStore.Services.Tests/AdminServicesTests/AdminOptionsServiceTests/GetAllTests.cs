using BMWStore.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetAllTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutOptions_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Empty(models);
        }

        [Fact]
        public void WithOption_ShouldReturnOption()
        {
            var dbContext = this.GetDbContext();
            SeedOptionsMethods.SeedOptionWithOptionType(dbContext);
            var service = this.GetService(dbContext);
            var models = service.GetAll();

            Assert.Single(models);
        }
    }
}

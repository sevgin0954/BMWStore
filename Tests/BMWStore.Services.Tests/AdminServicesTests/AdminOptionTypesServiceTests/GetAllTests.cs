using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class GetAllTests : BaseAdminOptionTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutModelTypes_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Empty(models);
        }

        [Fact]
        public void WithModelType_ShouldReturnOptionTypeWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Single(models);
            Assert.Equal(dbOptionType.Name, models.First().Name);
        }
    }
}

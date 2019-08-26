using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class GetAllTests : BaseAdminModelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public void WithoutModelTpyes_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Empty(models);
        }

        [Fact]
        public void WithModelType_ShouldReturnModelTypeWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbModelType = SeedModelTypesMethods.SeedModelType(dbContext);
            var service = this.GetService(dbContext);

            var models = service.GetAll();

            Assert.Single(models);
            Assert.Equal(dbModelType.Name, models.First().Name);
        }
    }
}

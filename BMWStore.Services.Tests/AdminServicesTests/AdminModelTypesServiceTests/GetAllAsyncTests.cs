using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminModelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutModelTpyes_ShouldReturnEmtpyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithModelType_ShouldReturnModelTypeWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbModelType = SeedModelTypesMethods.SeedModelType(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
            Assert.Equal(dbModelType.Name, models.First().Name);
        }
    }
}

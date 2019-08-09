using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminModelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutModelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithModelType_ShouldReturnModelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedModelTypesMethods.SeedModelType(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}

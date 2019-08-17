using BMWStore.Tests.Common.SeedTestMethods;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminOptionTypesServiceTest, IClassFixture<MapperFixture>
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
        public async void WithModelType_ShouldReturnOptionTypeWithCorrectName()
        {
            var dbContext = this.GetDbContext();
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
            Assert.Equal(dbOptionType.Name, models.First().Name);
        }
    }
}

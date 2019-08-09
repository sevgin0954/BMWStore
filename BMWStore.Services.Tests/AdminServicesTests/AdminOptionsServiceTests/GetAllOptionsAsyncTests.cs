using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class GetAllOptionsAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutOptions_ShouldReturnEmptyCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllOptionsAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithOption_ShouldReturnOption()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedOptionsMethods.SeedOption(dbContext);

            var models = await service.GetAllOptionsAsync();

            Assert.Single(models);
        }
    }
}

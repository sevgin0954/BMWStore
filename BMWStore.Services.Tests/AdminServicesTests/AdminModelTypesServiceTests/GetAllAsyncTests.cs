using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class GetAllAsyncTests : BaseAdminModelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutModelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithModelType_ShouldReturnModelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedModelType(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}

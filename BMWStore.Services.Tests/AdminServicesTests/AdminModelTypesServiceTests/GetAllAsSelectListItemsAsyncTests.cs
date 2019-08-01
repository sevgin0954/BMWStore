using System;
using BMWStore.Data;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class GetAllAsSelectListItemsAsyncTests : BaseAdminModelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsSelectListItemsAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutModelTypes_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var items = await service.GetAllAsSelectListItemsAsync();

            Assert.Empty(items);
        }

        [Fact]
        public async void WithModelType_ShouldReturnModelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            this.SeedModelType(dbContext);

            var items = await service.GetAllAsSelectListItemsAsync();

            Assert.Single(items);
        }
    }
}

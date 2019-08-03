﻿using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class GetAllAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public GetAllAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithoutEngines_ShouldReturnEmptyCollection()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Empty(models);
        }

        [Fact]
        public async void WithEngines_ShouldReturnEngines()
        {
            var dbContext = this.baseTest.GetDbContext();
            this.SeedEngineWithTransmission(dbContext);
            var service = this.GetService(dbContext);

            var models = await service.GetAllAsync();

            Assert.Single(models);
        }
    }
}
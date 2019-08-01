using BMWStore.Common.Constants;
using BMWStore.Models.EngineModels.BindingModels;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class EditAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public EditAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new AdminEngineEditBindingModel()
            {
                Id = Guid.NewGuid().ToString()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditEngine()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var engineId = Guid.NewGuid().ToString();
            var dbEngine = this.SeedEngine(engineId, dbContext);

            var model = new AdminEngineEditBindingModel()
            {
                Id = engineId,
                Name = Guid.NewGuid().ToString()
            };

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbEngine.Name);
        }
    }
}

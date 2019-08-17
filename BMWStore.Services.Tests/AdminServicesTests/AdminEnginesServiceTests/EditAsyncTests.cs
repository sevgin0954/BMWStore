using BMWStore.Common.Constants;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class EditAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
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
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbEngine = SeedEnginesMethods.SeedEngine(dbContext);

            var model = new AdminEngineEditBindingModel()
            {
                Id = dbEngine.Id,
                Name = Guid.NewGuid().ToString()
            };

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbEngine.Name);
        }
    }
}

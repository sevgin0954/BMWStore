using BMWStore.Models.EngineModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class CreateEngineAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewEngine()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new EngineBindingModel();

            await service.CreateEngineAsync(model);

            Assert.Single(dbContext.Engines);
        }
    }
}

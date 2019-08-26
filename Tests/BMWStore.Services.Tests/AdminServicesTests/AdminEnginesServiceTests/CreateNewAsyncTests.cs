using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class CreateNewAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewEngine()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new EngineServiceModel();

            await service.CreateNewAsync(model);

            Assert.Single(dbContext.Engines);
        }
    }
}

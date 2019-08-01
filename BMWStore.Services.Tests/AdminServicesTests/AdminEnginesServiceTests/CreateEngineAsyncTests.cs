using BMWStore.Models.EngineModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class CreateEngineAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateEngineAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewEngine()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminEngineCreateBindingModel();

            await service.CreateEngineAsync(model);

            Assert.Single(dbContext.Engines);
        }
    }
}

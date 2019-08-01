using BMWStore.Models.ModelTypeModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class CreateNewModelTypeTests : BaseAdminModelTypesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateNewModelTypeTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewModelType()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminModelTypeCreateBidningModel();

            await service.CreateNewModelType(model);

            Assert.Single(dbContext.ModelTypes);
        }
    }
}

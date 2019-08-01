using BMWStore.Models.OptionModels.BidningModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class CreateNewOptionAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateNewOptionAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewOption()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminOptionCreateBindingModel();

            await service.CreateNewOptionAsync(model);

            Assert.Single(dbContext.Options);
        }
    }
}

using BMWStore.Models.OptionModels.BidningModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class CreateNewOptionAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewOption()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminOptionCreateBindingModel();

            await service.CreateNewOptionAsync(model);

            Assert.Single(dbContext.Options);
        }
    }
}

using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class CreateNewAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewOption()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new OptionServiceModel();

            await service.CreateNewAsync(model);

            Assert.Single(dbContext.Options);
        }
    }
}

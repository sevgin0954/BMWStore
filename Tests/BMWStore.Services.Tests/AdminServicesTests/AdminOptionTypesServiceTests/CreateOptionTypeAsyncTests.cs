using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class CreateOptionTypeAsyncTests : BaseAdminOptionTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShoudCreateNewOptionType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new OptionTypeServiceModel();

            await service.CreateNewAsync(model);

            Assert.Single(dbContext.OptionTypes);
        }
    }
}

using BMWStore.Models.OptionTypeModels.BindingModels;
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
            var model = new OptionTypeCreateBindingModel();

            await service.CreateOptionTypeAsync(model);

            Assert.Single(dbContext.OptionTypes);
        }
    }
}

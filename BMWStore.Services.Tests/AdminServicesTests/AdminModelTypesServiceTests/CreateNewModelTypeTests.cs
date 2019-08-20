using BMWStore.Models.ModelTypeModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class CreateNewModelTypeTests : BaseAdminModelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewModelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new ModelTypeBindingModel();

            await service.CreateNewModelType(model);

            Assert.Single(dbContext.ModelTypes);
        }
    }
}

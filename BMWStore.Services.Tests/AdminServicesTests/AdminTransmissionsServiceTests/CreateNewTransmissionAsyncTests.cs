using BMWStore.Models.TransmissionsModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class CreateNewTransmissionAsyncTests : BaseAdminTransmissionsServiceTests
    {
        [Fact]
        public async void WithModel_ShouldCreateNewTransmission()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminTransmissionsCreateBindingModel();

            await service.CreateNewTransmissionAsync(model);

            Assert.Single(dbContext.Transmissions);
        }
    }
}

using BMWStore.Services.Models;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class CreateNewAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithModel_ShouldCreateNewTransmission()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new TransmissionServiceModel();

            await service.CreateNewAsync(model);

            Assert.Single(dbContext.Transmissions);
        }
    }
}

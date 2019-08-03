using BMWStore.Models.TransmissionsModels.BindingModels;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class CreateNewTransmissionAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public CreateNewTransmissionAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithModel_ShouldCreateNewTransmission()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminTransmissionsCreateBindingModel();

            await service.CreateNewTransmissionAsync(model);

            Assert.Single(dbContext.Transmissions);
        }
    }
}

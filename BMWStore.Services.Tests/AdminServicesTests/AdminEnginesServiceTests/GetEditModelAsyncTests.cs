using BMWStore.Common.Constants;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class GetEditModelAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert
                .ThrowsAsync<ArgumentException>(async () => await service.GetEditModelAsync(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithEngineWithTransmission_ShouldReturnModelWithCorrectSelectedTransmissionId()
        {
            var dbContext = this.GetDbContext();
            var dbEngine = SeedEnginesMethods.SeedEngineWithTransmission(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetEditModelAsync(dbEngine.Id);

            Assert.Equal(dbEngine.Transmission.Id, model.SelectedTransmissionId);
        }
    }
}

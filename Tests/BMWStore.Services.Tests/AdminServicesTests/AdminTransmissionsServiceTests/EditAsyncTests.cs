using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminTransmissionsServiceTests
{
    public class EditAsyncTests : BaseAdminTransmissionsServiceTests, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();
            var model = TransmissionServiceModelMethods.Create(incorrectId);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditTransmissionName()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTransmision = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var model = TransmissionServiceModelMethods.Create(dbTransmision.Id);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbTransmision.Name);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditTransmissionPrice()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTransmision = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var model = TransmissionServiceModelMethods.Create(dbTransmision.Id);

            await service.EditAsync(model);

            Assert.Equal(model.Price, dbTransmision.Price);
        }
    }
}

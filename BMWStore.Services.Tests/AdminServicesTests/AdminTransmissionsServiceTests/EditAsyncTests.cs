using BMWStore.Common.Constants;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Services.Tests.Common.SeedTestMethods;
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
            var model = this.GetModel(incorrectId);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditTransmissionName()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTransmision = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var model = this.GetModel(dbTransmision.Id);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbTransmision.Name);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditTransmissionPrice()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbTransmision = SeedTransmissionsMethods.SeedTransmission(dbContext);
            var model = this.GetModel(dbTransmision.Id);

            await service.EditAsync(model);

            Assert.Equal(model.Price, dbTransmision.Price);
        }

        private TransmissionEditBindingModel GetModel(string id)
        {
            var model = new TransmissionEditBindingModel()
            {
                Id = id,
                Name = Guid.NewGuid().ToString(),
                Price = 1
            };

            return model;
        }
    }
}

using BMWStore.Common.Constants;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class EditAsyncTests : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new FuelTypeEditBindingModel()
            {
                Id = Guid.NewGuid().ToString()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbFuelType = SeedFuelTypesMethods.SeedFuelType(dbContext);

            var model = new FuelTypeEditBindingModel()
            {
                Id = dbFuelType.Id,
                Name = Guid.NewGuid().ToString()
            };

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbFuelType.Name);
        }
    }
}

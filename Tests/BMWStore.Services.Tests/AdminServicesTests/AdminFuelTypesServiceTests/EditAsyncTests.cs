using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.SeedTestMethods;
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
            var id = Guid.NewGuid().ToString();

            var model = FuelTypeServiceModelMethods.Create(id);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbFuelType = SeedFuelTypesMethods.SeedFuelType(dbContext);
            var name = Guid.NewGuid().ToString();

            var model = FuelTypeServiceModelMethods.Create(dbFuelType.Id, name);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbFuelType.Name);
        }
    }
}

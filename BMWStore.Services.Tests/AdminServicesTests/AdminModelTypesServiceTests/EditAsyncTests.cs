using BMWStore.Common.Constants;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminModelTypesServiceTests
{
    public class EditAsyncTests : BaseAdminModelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new ModelTypeEditBindingModel()
            {
                Id = Guid.NewGuid().ToString()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditEngine()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbModelType = SeedModelTypesMethods.SeedModelType(dbContext);

            var model = new ModelTypeEditBindingModel()
            {
                Id = dbModelType.Id,
                Name = Guid.NewGuid().ToString()
            };

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbModelType.Name);
        }
    }
}

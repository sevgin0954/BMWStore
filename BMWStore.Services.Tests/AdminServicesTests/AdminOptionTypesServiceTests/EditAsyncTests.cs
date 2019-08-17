using BMWStore.Common.Constants;
using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class EditAsyncTests : BaseAdminOptionTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = new OptionTypeEditBindingModel()
            {
                Id = Guid.NewGuid().ToString()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditOptionType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);

            var model = new OptionTypeEditBindingModel()
            {
                Id = dbOptionType.Id,
                Name = Guid.NewGuid().ToString()
            };

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbOptionType.Name);
        }
    }
}

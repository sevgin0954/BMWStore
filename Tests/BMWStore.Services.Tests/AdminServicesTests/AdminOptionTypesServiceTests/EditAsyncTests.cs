using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
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
            var id = Guid.NewGuid().ToString();

            var model = OptionTypeServiceModelMethods.Create(id);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditOptionType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var name = Guid.NewGuid().ToString();

            var model = OptionTypeServiceModelMethods.Create(dbOptionType.Id, name);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbOptionType.Name);
        }
    }
}

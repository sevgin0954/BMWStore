using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class EditAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = OptionServiceModelMethods.Create(Guid.NewGuid().ToString());

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditOption()
        {
            var dbContext = this.GetDbContext();
            var dbOption = SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);

            var modelName = Guid.NewGuid().ToString();
            var model = OptionServiceModelMethods.Create(dbOption.Id, modelName);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbOption.Name);
        }
    }
}
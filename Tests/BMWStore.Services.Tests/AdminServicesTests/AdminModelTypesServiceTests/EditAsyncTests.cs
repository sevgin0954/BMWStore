using BMWStore.Common.Constants;
using BMWStore.Tests.Common.CreateMethods;
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
            var id = Guid.NewGuid().ToString();

            var model = ModelTypeServiceModelMethods.Create(id);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditEngine()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var dbModelType = SeedModelTypesMethods.SeedModelType(dbContext);
            var name = Guid.NewGuid().ToString();

            var model = ModelTypeServiceModelMethods.Create(dbModelType.Id, name);

            await service.EditAsync(model);

            Assert.Equal(model.Name, dbModelType.Name);
        }
    }
}

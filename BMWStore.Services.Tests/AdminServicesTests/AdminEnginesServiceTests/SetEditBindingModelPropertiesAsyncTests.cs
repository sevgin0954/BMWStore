using BMWStore.Common.Constants;
using BMWStore.Models.EngineModels.BindingModels;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class SetEditBindingModelPropertiesAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = new AdminEngineEditBindingModel()
            {
                Id = Guid.NewGuid().ToString()
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.SetEditBindingModelPropertiesAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }
    }
}

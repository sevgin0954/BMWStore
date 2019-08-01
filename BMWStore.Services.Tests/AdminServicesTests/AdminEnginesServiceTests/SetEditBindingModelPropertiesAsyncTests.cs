using BMWStore.Common.Constants;
using BMWStore.Models.EngineModels.BindingModels;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminEnginesServiceTests
{
    public class SetEditBindingModelPropertiesAsyncTests : BaseAdminEnginesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public SetEditBindingModelPropertiesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
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

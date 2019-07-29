using BMWStore.Models.CarModels.BindingModels;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class SetEditBindingModelPropertiesAsyncTests: BaseAdminCarsServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public SetEditBindingModelPropertiesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithIncorrectModelId_ShouldThrowException()
        {
            var dbContext = this.baseTest.GetDbContext();
            var service = this.GetService(dbContext);
            var model = this.GetAdminCarModel(Guid.NewGuid().ToString());

            await Assert.ThrowsAnyAsync<Exception>(async () => await service.SetEditBindingModelPropertiesAsync(model));
        }

        private AdminCarEditBindingModel GetAdminCarModel(string id)
        {
            var model = new AdminCarEditBindingModel()
            {
                Id = id
            };

            return model;
        }
    }
}

using BMWStore.Common.Constants;
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Services.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionsServiceTests
{
    public class EditOptionAsyncTests : BaseAdminOptionsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var model = this.GetEditModel(Guid.NewGuid().ToString());

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditOptionAsync(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldEditOption()
        {
            var dbContext = this.GetDbContext();
            var dbOption = SeedOptionsMethods.SeedOption(dbContext);
            var service = this.GetService(dbContext);

            var modelName = Guid.NewGuid().ToString();
            var model = this.GetEditModel(dbOption.Id, modelName);

            await service.EditOptionAsync(model);

            Assert.Equal(model.Name, dbOption.Name);
        }

        private AdminCarOptionEditBindingModel GetEditModel(string id, string name = null)
        {
            var model = new AdminCarOptionEditBindingModel()
            {
                Id = id,
                Name = name
            };

            return model;
        }
    }
}

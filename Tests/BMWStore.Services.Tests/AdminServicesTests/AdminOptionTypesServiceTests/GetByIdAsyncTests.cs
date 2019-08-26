using BMWStore.Common.Constants;
using BMWStore.Models.OptionTypeModels.ViewModels;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminOptionTypesServiceTests
{
    public class GetByIdAsyncTests : BaseAdminOptionTypesServiceTest
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            var incorrectId = Guid.NewGuid().ToString();

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => 
                await service.GetByIdAsync<OptionTypeConciseViewModel>(incorrectId));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithCorrectId_ShouldReturnOptionType()
        {
            var dbContext = this.GetDbContext();
            var dbOptionType = SeedOptionTypesMethods.SeedOptionType(dbContext);
            var service = this.GetService(dbContext);

            var model = await service.GetByIdAsync<OptionTypeConciseViewModel>(dbOptionType.Id);

            Assert.NotNull(model);
        }
    }
}

using BMWStore.Services.Tests.Common.SeedTestMethods;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminFuelTypesServiceTests
{
    public class GetFuelTypesViewModelAsync : BaseAdminFuelTypesServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithoutFuelTypes_ShouldReturnModelWithEmptyFuelTypesCollection()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetFuelTypesViewModelAsync(1);

            Assert.Empty(model.FuelTypes);
        }

        [Fact]
        public async void WithFuelType_ShoudReturnModelWithFuelType()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedFuelTypesMethods.SeedFuelType(dbContext);

            var model = await service.GetFuelTypesViewModelAsync(1);

            Assert.Single(model.FuelTypes);
        }

        [Fact]
        public async void WithPageNumber_ShouldReturnModelWithCorrectCurrentPage()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var pageNumber = 1;
            var model = await service.GetFuelTypesViewModelAsync(pageNumber);

            Assert.Equal(pageNumber, model.CurrentPage);
        }

        [Fact]
        public async void WithoutFuelTypes_ShouldReturnModelWithZeroTotalPagesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);

            var model = await service.GetFuelTypesViewModelAsync(1);

            Assert.Equal(0, model.TotalPagesCount);
        }

        [Fact]
        public async void WithFuelType_ShouldReturnModelWithCorrectTotalPagesCount()
        {
            var dbContext = this.GetDbContext();
            var service = this.GetService(dbContext);
            SeedFuelTypesMethods.SeedFuelType(dbContext);

            var model = await service.GetFuelTypesViewModelAsync(1);

            Assert.Equal(1, model.TotalPagesCount);
        }
    }
}

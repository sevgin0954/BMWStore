using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Exceptions.Repositories;
using BMWStore.Tests.Common.CreateMethods;
using BMWStore.Tests.Common.SeedTestMethods;
using System;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class EditAsyncTests : BaseAdminCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var incorrectId = Guid.NewGuid().ToString();
            var model = CarServiceModelCreateMethods.Create(incorrectId);
            var service = this.GetService(dbContext);

            var exception = 
                await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditAsync<UsedCar>(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithoutPictures_ShouldNotChangePictures()
        {
            var dbContext = this.GetDbContext();
            var dbPicture = EntitiesCreateMethods.CreatePicture(Guid.NewGuid().ToString());
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbPicture);

            var service = this.GetService(dbContext);
            var model = CarServiceModelCreateMethods.Create(dbCar.Id);

            var exception = await Assert.ThrowsAsync<RepositoryUpdateNoRowsAffectedException>(
                async () => await service.EditAsync<NewCar>(model));
            Assert.Equal(ErrorConstants.UnitOfWorkNoRowsAffected, exception.Message);
        }

        [Fact]
        public async void WithPictures_ShouldReplacePictures()
        {
            var dbContext = this.GetDbContext();
            var dbPictures = EntitiesCreateMethods.CreatePicture(Guid.NewGuid().ToString());
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbPictures);

            var picturePublicId = Guid.NewGuid().ToString();
            var model = CarServiceModelCreateMethods.Create(dbCar.Id, picturePublicId);
            var service = this.GetService(dbContext);

            await service.EditAsync<NewCar>(model);

            Assert.Single(dbContext.Pictures);
            Assert.Equal(picturePublicId, dbContext.Pictures.First().PublicId);
        }

        [Fact]
        public async void WithOptions_ShouldReplaceOptions()
        {
            var dbContext = this.GetDbContext();
            var carOption = SeedOptionsMethods.SeedOption(dbContext);
            var dbCarOptions = EntitiesCreateMethods.CreateCarOption(carOption);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbCarOptions);

            var service = this.GetService(dbContext);

            var inputOption = SeedOptionsMethods.SeedOption(dbContext);
            var model = CarServiceModelCreateMethods.Create(dbCar.Id, inputOption);

            await service.EditAsync<NewCar>(model);

            Assert.True(dbCar.Options.All(o => inputOption.Id == o.OptionId));
        }

        [Fact]
        public async void WithUsedCarAndModelWithMileage_ShouldEditMiles()
        {
            var dbContext = this.GetDbContext();
            var dbCar = SeedCarsMethods.SeedCar<UsedCar>(dbContext);

            var service = this.GetService(dbContext);
            var mileage = 10.0;
            var model = CarServiceModelCreateMethods.Create(dbCar.Id, mileage);

            await service.EditAsync<UsedCar>(model);

            Assert.Equal(mileage, dbCar.Mileage);
        }
    }
}
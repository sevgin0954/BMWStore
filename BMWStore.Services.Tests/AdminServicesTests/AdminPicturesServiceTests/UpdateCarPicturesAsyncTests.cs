using BMWStore.Data;
using BMWStore.Entities;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminPicturesServiceTests
{
    public class UpdateCarPicturesAsyncTests : BaseAdminPicturesServiceTest, IClassFixture<BaseTestFixture>
    {
        private readonly BaseTestFixture baseTest;

        public UpdateCarPicturesAsyncTests(BaseTestFixture baseTest)
        {
            this.baseTest = baseTest;
        }

        [Fact]
        public async void WithPictures_ShouldReplacePictures()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbPicuture = this.CreatePicture(dbContext);
            var dbCar = this.SeedCar(dbContext, dbPicuture);
            var newPictureUrl = Guid.NewGuid().ToString();

            await this.CallUpdateCarPicturesAsync(dbContext, dbCar, newPictureUrl);

            Assert.Single(dbCar.Pictures);
            Assert.Equal(newPictureUrl, dbCar.Pictures.First().PublicId);
        }

        [Fact]
        public async void WithPictures_ShouldChangePicturesStateToDeleted()
        {
            var dbContext = this.baseTest.GetDbContext();
            var dbPicuture = this.CreatePicture(dbContext);
            var dbCar = this.SeedCar(dbContext, dbPicuture);
            var newPictureUrl = Guid.NewGuid().ToString();

            await this.CallUpdateCarPicturesAsync(dbContext, dbCar, newPictureUrl);

            var currentDbPictureState = dbContext.Entry(dbPicuture).State;
            Assert.Equal(EntityState.Deleted, currentDbPictureState);
        }

        private BaseCar SeedCar(ApplicationDbContext dbContext, Picture dbPicuture)
        {
            var dbCar = new NewCar();
            dbCar.Pictures.Add(dbPicuture);

            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        private async Task CallUpdateCarPicturesAsync(ApplicationDbContext dbContext, BaseCar dbCar, string newPictureUrl)
        {
            var service = this.GetService(dbContext, newPictureUrl);

            var inputPictures = new Mock<IEnumerable<IFormFile>>().Object;
            await service.UpdateCarPicturesAsync(dbCar, inputPictures);
        }

        private IAdminPicturesService GetService(ApplicationDbContext dbContext, string newPictureUrl)
        {
            var mockedCloudinaryService = new Mock<ICloudinaryService>();
            CommonMockServicesTestMethods.SetupMockedCludinaryServiceUploadPicturesAsync(mockedCloudinaryService, newPictureUrl);
            var service = this.GetService(dbContext, mockedCloudinaryService.Object);

            return service;
        }
    }
}

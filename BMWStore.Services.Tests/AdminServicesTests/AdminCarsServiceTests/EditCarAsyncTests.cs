using BMWStore.Common.Constants;
using BMWStore.Entities;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Tests.Common.SeedTestMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public class EditCarAsyncTests : BaseAdminCarsServiceTest, IClassFixture<MapperFixture>
    {
        [Fact]
        public async void WithIncorrectId_ShouldThrowException()
        {
            var dbContext = this.GetDbContext();
            var incorrectId = Guid.NewGuid().ToString();
            var model = this.CreateCarEditModel(incorrectId);
            var service = this.GetService(dbContext);

            var exception = 
                await Assert.ThrowsAsync<ArgumentException>(async () => await service.EditCarAsync<UsedCar>(model));
            Assert.Equal(ErrorConstants.IncorrectId, exception.Message);
        }

        [Fact]
        public async void WithoutPictures_ShouldNotChangePictures()
        {
            var dbContext = this.GetDbContext();
            var dbPicture = this.CreatePictur(Guid.NewGuid().ToString());
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbPicture);

            var model = this.CreateCarEditModel(dbCar.Id);
            var service = this.GetService(dbContext);

            await service.EditCarAsync<NewCar>(model);

            Assert.Single(dbCar.Pictures);
            Assert.Equal(dbCar.Pictures.First().PublicId, dbPicture.PublicId);
        }

        [Fact]
        public async void WithPictures_ShouldReplacePictures()
        {
            var dbContext = this.GetDbContext();
            var dbPictures = this.CreatePictur(Guid.NewGuid().ToString());
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbPictures);

            var inputPicture = this.CreateInputPicture();
            var model = this.CreateCarEditModel(dbCar.Id, inputPicture);
            var mockedPictureService = new Mock<IAdminPicturesService>();
            var service = this.GetService(dbContext, mockedPictureService.Object);

            await service.EditCarAsync<NewCar>(model);

            mockedPictureService.Verify(s => s.UpdateCarPicturesAsync(
                It.Is<BaseCar>(c => c.Id == dbCar.Id), model.Pictures), Times.Once);
        }

        [Fact]
        public async void WithoutOptions_ShouldNotReplaceOptions()
        {
            var dbContext = this.GetDbContext();
            var dbOptions = this.CreateOptions(2);
            var dbCarOptions = this.CreateCarOptions(dbOptions);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbCarOptions);

            var service = this.GetService(dbContext);

            var model = this.CreateCarEditModel(dbCar.Id);
            await service.EditCarAsync<NewCar>(model);

            Assert.Equal(dbCarOptions, dbCar.Options);
        }

        [Fact]
        public async void WithOptions_ShouldReplaceOptions()
        {
            var dbContext = this.GetDbContext();
            var dbOptions = this.CreateOptions(2);
            var selectedOptions = dbOptions.Take(1);
            var dbCarOptions = this.CreateCarOptions(selectedOptions);
            var dbCar = SeedCarsMethods.SeedCar<NewCar>(dbContext, dbCarOptions);

            var service = this.GetService(dbContext);

            var selectedInputOptions = dbOptions.Skip(1);
            var model = this.CreateCarEditModel(dbCar.Id, selectedInputOptions);
            await service.EditCarAsync<NewCar>(model);

            Assert.True(dbCar.Options.All(o => selectedInputOptions.Any(sio => sio.Id == o.OptionId)));
        }

        private AdminCarBindingModel CreateCarEditModel(string id)
        {
            var model = new AdminCarBindingModel()
            {
                Id = id
            };

            return model;
        }

        private AdminCarBindingModel CreateCarEditModel(string id, params IFormFile[] pictures)
        {
            var model =  new AdminCarBindingModel()
            {
                Id = id,
                Pictures = pictures
            };

            return model;
        }

        private AdminCarBindingModel CreateCarEditModel(string id, IEnumerable<Option> options)
        {
            var model = new AdminCarBindingModel()
            {
                Id = id,
                CarOptions = options.Select(o => new SelectListItem() { Text = o.Name })
            };

            return model;
        }

        private IFormFile CreateInputPicture()
        {
            return new Mock<IFormFile>().Object;
        }

        private ICollection<CarOption> CreateCarOptions(IEnumerable<Option> options)
        {
            var carOptions = new List<CarOption>();

            foreach (var option in options)
            {
                var carOption = new CarOption()
                {
                    Option = option
                };
                carOptions.Add(carOption);
            }

            return carOptions;
        }
        private ICollection<Option> CreateOptions(int count)
        {
            var options = new List<Option>();

            for (int i = 0; i < count; i++)
            {
                var option = new Option()
                {
                    Name = Guid.NewGuid().ToString()
                };
                options.Add(option);
            }

            return options;
        }

        private Picture CreatePictur(string publicId)
        {
            var dbPicture = new Picture()
            {
                PublicId = publicId
            };

            return dbPicture;
        }
    }
}

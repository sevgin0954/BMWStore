using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.AdminServices;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Moq;
using System.Collections.Generic;

namespace BMWStore.Services.Tests.AdminServicesTests.AdminCarsServiceTests
{
    public abstract class BaseAdminCarsServiceTest
    {
        protected IAdminCarsService GetService(ApplicationDbContext dbContext)
        {
            var pictureService = new Mock<IAdminPicturesService>().Object;

            return this.GetService(dbContext, pictureService);
        }

        protected IAdminCarsService GetService(ApplicationDbContext dbContext, IAdminPicturesService pictureService)
        {
            var carsRepository = new CarRepository(dbContext);
            var carOptionsRepository = new CarOptionRepository(dbContext);
            var selectListItemsService = new Mock<ISelectListItemsService>().Object;

            var service = new AdminCarsService(carsRepository, carOptionsRepository, pictureService, selectListItemsService);

            return service;
        }

        protected NewCar SeedNewCar(ApplicationDbContext dbContext, ICollection<Picture> pictures)
        {
            var dbCar = new NewCar()
            {
                Pictures = pictures
            };
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        protected NewCar SeedNewCar(ApplicationDbContext dbContext, ICollection<CarOption> options)
        {
            var dbCar = new NewCar()
            {
                Options = options
            };
            dbContext.BaseCars.Add(dbCar);
            dbContext.SaveChanges();

            return dbCar;
        }

        protected string SeedCar<TCar>(ApplicationDbContext dbContext) where TCar : BaseCar, new()
        {
            var newCar = new TCar();
            dbContext.BaseCars.Add(newCar);
            dbContext.SaveChanges();

            return newCar.Id;
        }
    }
}

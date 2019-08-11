using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common.MockTestMethods;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public abstract class BaseCarsServiceTest : BaseTest
    {
        protected ICarsService GetService(ApplicationDbContext dbContext)
        {
            var userManager = CommonMockTestMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockTestMethods.GetMockedSignInManager(userManager).Object;
            var carRepository = new CarRepository(dbContext);
            var service = new CarsService(signInManager, carRepository);

            return service;
        }

        protected ICarsService GetService(SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            var carRepository = new CarRepository(dbContext);
            var service = new CarsService(signInManager, carRepository);

            return service;
        }
    }
}

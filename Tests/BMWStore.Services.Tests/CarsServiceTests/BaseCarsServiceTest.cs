using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Tests.Common.MockMethods;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public abstract class BaseCarsServiceTest : BaseTest
    {
        protected ICarsService GetService(ApplicationDbContext dbContext)
        {
            var userManager = CommonMockMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockMethods.GetMockedSignInManager(userManager).Object;

            var service = this.GetService(signInManager, dbContext);

            return service;
        }

        protected ICarsService GetService(SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            var readService = new ReadService(dbContext);
            var carRepository = new CarRepository(dbContext);
            var service = new CarsService(signInManager, carRepository, readService);

            return service;
        }
    }
}

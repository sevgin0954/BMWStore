using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public abstract class BaseCarsServiceTest
    {
        protected ICarsService GetService()
        {
            var userManager = CommonMockTestMethods.GetMockedUserManager().Object;
            var signInManager = CommonMockTestMethods.GetMockedSignInManager(userManager).Object;
            var service = new CarsService(signInManager);

            return service;
        }

        protected ICarsService GetService(SignInManager<User> signInManager)
        {
            var service = new CarsService(signInManager);

            return service;
        }
    }
}

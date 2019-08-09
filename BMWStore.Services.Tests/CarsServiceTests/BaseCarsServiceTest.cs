using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common.MockTestMethods;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.CarsServiceTests
{
    public abstract class BaseCarsServiceTest : BaseTest
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

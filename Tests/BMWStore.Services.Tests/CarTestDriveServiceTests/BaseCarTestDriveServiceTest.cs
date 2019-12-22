using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;
using BMWStore.Tests.Common.MockMethods;
using Microsoft.AspNetCore.Identity;

namespace BMWStore.Services.Tests.CarTestDriveServiceTests
{
	public class BaseCarTestDriveServiceTest : BaseTest
	{
		protected ICarTestDriveService GetService(
			ApplicationDbContext dbContext,
			SignInManager<User> signInManager, 
			UserManager<User> userManager)
		{
			var userRepository = new UserRepository(dbContext);
			var carRepository = new CarRepository(dbContext);
			var service = new CarTestDriveService(signInManager, userManager, userRepository, carRepository);

			return service;
		}

		protected SignInManager<User> GetSetupedSignInManager(bool isSignIn)
		{
			var mockedSignInManager = CommonMockMethods.GetMockedSignInManager();
			CommonMockMethods.SetupMockedSignInManagerIsSignIn(mockedSignInManager, isSignIn);

			return mockedSignInManager.Object;
		}

		protected UserManager<User> GetSetupedUserManager(string userId)
		{
			var mockedUserManger = CommonMockMethods.GetMockedUserManager();
			CommonMockMethods.SetupMockedUserManagerGetUserId(mockedUserManger, userId);

			return mockedUserManger.Object;
		}
	}
}

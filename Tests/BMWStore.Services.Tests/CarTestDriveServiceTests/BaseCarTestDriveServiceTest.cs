using BMWStore.Data;
using BMWStore.Data.Repositories;
using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Tests.Common;
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
	}
}

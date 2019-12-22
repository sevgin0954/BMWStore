using BMWStore.Common.Constants;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Helpers;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BMWStore.Services
{
	public class CarTestDriveService : ICarTestDriveService
	{
		private readonly SignInManager<User> signInManager;
		private readonly UserManager<User> userManager;
		private readonly IUserRepository userRepository;
		private readonly ICarRepository carRepository;

		public CarTestDriveService(
			SignInManager<User> signInManager,
			UserManager<User> userManager,
			IUserRepository userRepository,
			ICarRepository carRepository)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.userRepository = userRepository;
			this.carRepository = carRepository;
		}

		public async Task<TModel> GetCarTestDriveModelById<TModel>(string id, ClaimsPrincipal user)
			 where TModel : BaseCarTestDriveServiceModel
		{
			var car = this.carRepository
				.Find(c => c.Id == id);
			var carModel = await (await this.GetCarTestDriveModelAsync<TModel>(car, user, 1)).FirstOrDefaultAsync();

			DataValidator.ValidateNotNull(carModel, new ArgumentException(ErrorConstants.IncorrectId));

			return carModel;
		}

		public async Task<IQueryable<TModel>> GetCarTestDriveModelAsync<TModel>(
			IQueryable<BaseCar> cars,
			ClaimsPrincipal user,
			int pageNumber) where TModel : BaseCarTestDriveServiceModel
		{
			var isUserSignedIn = this.signInManager.IsSignedIn(user);

			var dbUserId = this.userManager.GetUserId(user);
			var dbUserTestDriveIds = await this.userRepository.GetAll()
				.Where(u => u.Id == dbUserId)
				.SelectMany(u => u.TestDrives.Select(td => td.Id))
				.ToArrayAsync();

			var carModels = cars
				.GetFromPage(pageNumber, WebConstants.PageSize)
				.To<TModel>(new { isUserSignedIn, dbUserTestDriveIds });

			return carModels;
		}
	}
}

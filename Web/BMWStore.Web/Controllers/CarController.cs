using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class CarController : Controller
    {
		private readonly ICarTestDriveService carTestDriveService;

		public CarController(ICarTestDriveService carTestDriveService)
        {
			this.carTestDriveService = carTestDriveService;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string carId)
        {
            try
            {
                var carServiceModel = await this.carTestDriveService
					.GetCarTestDriveModelById<CarServiceModel>(carId, this.User);
                var carViewModel = Mapper.Map<CarViewModel>(carServiceModel);

                return View(carViewModel);
            }
            catch (ArgumentException)
            {
                this.TempData[WebConstants.StatusMessagePrefix] = ErrorConstants.IncorrectId;
                return Redirect("/");
            }
        }
    }
}
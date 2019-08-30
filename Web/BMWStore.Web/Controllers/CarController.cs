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
        private readonly ICarsService carsService;

        public CarController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string carId)
        {
            try
            {
                var carServiceModel = await this.carsService.GetCarTestDriveModelById<CarServiceModel>(carId, this.User);
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
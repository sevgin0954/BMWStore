﻿using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IOptionTypeService optionTypeService;

        public CarController(ICarsService carsService, IOptionTypeService optionTypeService)
        {
            this.carsService = carsService;
            this.optionTypeService = optionTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string carId)
        {
            var carModel = await this.carsService.GetCarViewModelAsync(carId);
            var optionTypeModels = this.optionTypeService.GetViewModels(carModel.Options);

            var model = new CarInventoryViewModel()
            {
                Car = carModel,
                OptionTypes = optionTypeModels
            };

            return View(model);
        }
    }
}
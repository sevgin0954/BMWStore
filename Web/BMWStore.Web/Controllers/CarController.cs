using AutoMapper;
using BMWStore.Models.CarInventoryModels.ViewModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            var carServiceModel = await this.carsService.GetCarTestDriveModelById(carId, this.User);
            var carViewModel = Mapper.Map<CarViewModel>(carServiceModel);
            var optionTypeModels = this.optionTypeService.GetViewModels(carViewModel.Options);

            var model = new CarInventoryViewModel()
            {
                Car = carViewModel,
                OptionTypes = optionTypeModels
            };

            return View(model);
        }
    }
}
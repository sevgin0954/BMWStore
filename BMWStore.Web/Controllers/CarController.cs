using BMWStore.Models.CarInvertoryModels.ViewModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            var carModel = await this.carsService.GetCarViewModelAsync(carId);

            var model = new CarInvertoryViewModel()
            {
                Car = carModel,
                ModelTypeName = carModel.ModelTypeName,
                ModelTypeId = carModel.ModelTypeId,
                Year = carModel.Year,
                isNew = carModel.IsNew
            };

            return View(model);
        }
    }
}
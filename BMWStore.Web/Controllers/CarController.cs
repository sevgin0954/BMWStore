using BMWStore.Models.CarInventoryModels.ViewModels;
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

        public CarController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string carId)
        {
            var carModel = await this.carsService.GetCarViewModelAsync(carId);

            var model = new CarInventoryViewModel()
            {
                Car = carModel,
                OptionTypes = carModel.Options.GroupBy(o => o.OptionTypeName).Select(group => new OptionTypeViewModel()
                {
                    Name = group.Key,
                    OptionNames = group.Select(o => o.Name).ToList()
                })
            };

            return View(model);
        }
    }
}
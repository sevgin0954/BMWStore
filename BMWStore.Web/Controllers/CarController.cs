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
            var model = await this.carsService.GetCarViewModel(carId);

            return View(model);
        }
    }
}
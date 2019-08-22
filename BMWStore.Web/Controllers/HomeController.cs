using BMWStore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;
using BMWStore.Models.HomeModels.ViewModels;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Models.CarInventoryModels.BindingModels;

namespace BMWStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly ICarRepository carRepository;

        public HomeController(IHomeService homeService, ICarRepository carRepository)
        {
            this.homeService = homeService;
            this.carRepository = carRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var newCars = this.carRepository.Set<NewCar>().AsQueryable();

            var searchModel = await this.homeService.GetSearchModelAsync(newCars);
            var model = new HomeViewModel();
            model.SearchModel = searchModel;
            model.TargetUrlsPublicIds.Add(
                "/Car?carId=34afd512-e702-4eb5-bb64-a1ac7dd3863e",
                "nsgkdld26bxsjsdg2cpv");

            return View(model);
        }

        [HttpGet]
        public IActionResult Search(CarType inventory, CarsInventoryBindingModel model)
        {
            var controllerName = CarType.NewCar == inventory ? "NewInventory" : "UsedInventory";

            return RedirectToAction("Index", controllerName, model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

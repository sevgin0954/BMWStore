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
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Helpers;
using BMWStore.Common.Helpers;
using BMWStore.Common.Constants;

namespace BMWStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;
        private readonly ICarRepository carRepository;
        private readonly INewCarRepository newCarRepository;
        private readonly IUsedCarRepository usedCarRepository;

        public HomeController(
            IHomeService homeService, 
            ICarRepository carRepository,
            INewCarRepository newCarRepository,
            IUsedCarRepository usedCarRepository)
        {
            this.homeService = homeService;
            this.carRepository = carRepository;
            this.newCarRepository = newCarRepository;
            this.usedCarRepository = usedCarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCars = this.carRepository.GetAll();

            var searchModel = await this.homeService.GetSearchModelAsync(allCars, CarType.NewCar);
            var model = new HomeViewModel();
            model.SearchModel = searchModel;
            model.TargetUrlsPublicIds.Add(
                "/Car?carId=34afd512-e702-4eb5-bb64-a1ac7dd3863e",
                "nsgkdld26bxsjsdg2cpv");

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> Filter(HomeSearchBindingModel model)
        {
            var priceRanges = ParameterParser.ParsePriceRange(model.SelectedPriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.SelectedYear, priceRanges[0], priceRanges[1], WebConstants.AllFilterTypeModelValue);

            var mutipleFilterStrategy = CarMultipleFilterStrategyFactory.GetStrategy(new string[] { model.SelectedModelType });

            var filteredCars = this.carRepository.GetFiltered(filterStrategies.ToArray());
            filteredCars = mutipleFilterStrategy.Filter(filteredCars);

            var searchModel = await this.homeService.GetSearchModelAsync(filteredCars, model.SelectedCarType);

            return Json(searchModel);
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

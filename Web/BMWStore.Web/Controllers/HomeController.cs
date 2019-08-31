using BMWStore.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;
using BMWStore.Models.HomeModels.ViewModels;
using BMWStore.Data.Repositories.Interfaces;
using System.Linq;
using BMWStore.Common.Enums;
using BMWStore.Models.CarInventoryModels.BindingModels;
using BMWStore.Models.HomeModels.BindingModel;
using BMWStore.Web.Factories.FilterStrategyFactory;
using BMWStore.Helpers;
using BMWStore.Common.Constants;
using AutoMapper;
using BMWStore.Entities;

namespace BMWStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string MainPhotoTargetUrl = "/Car?carId=34afd512-e702-4eb5-bb64-a1ac7dd3863e";
        private const string MainPhotoPublicId = "nsgkdld26bxsjsdg2cpv";

        private readonly IHomeService homeService;
        private readonly ICarsService carsService;
        private readonly ICarRepository carRepository;
        private readonly ICacheService cacheService;

        public HomeController(
            IHomeService homeService, 
            ICarsService carsService,
            ICarRepository carRepository,
            ICacheService cacheService)
        {
            this.homeService = homeService;
            this.carsService = carsService;
            this.carRepository = carRepository;
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCars = this.carRepository.GetAll();

            var searchServiceModel = await this.homeService.GetSearchModelAsync(allCars, CarType.NewCar);
            var searchBindingModel = Mapper.Map<HomeSearchBindingModel>(searchServiceModel);
            FilterTypeHelper.SelectFilterTypes(searchBindingModel.CarTypes, CarType.NewCar.ToString());

            var model = new HomeViewModel();
            model.SearchModel = searchBindingModel;
            model.TargetUrlsPublicIds.Add(
                MainPhotoTargetUrl,
                MainPhotoPublicId);

            return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> Filter(HomeSearchBindingModel model)
        {
            var cacheKey = KeyGenerator.Generate(
                WebConstants.CacheHomeFilterPrepend,
                model.SelectedCarType.ToString(),
                model.SelectedModelType,
                model.SelectedPriceRange,
                model.SelectedYear);

            var cachedModel = await this.cacheService.GetOrDefaultAsync<HomeSearchBindingModel>(cacheKey);
            if (cachedModel != null)
            {
                return Json(cachedModel);
            }

            var priceRanges = ParameterParser.ParsePriceRange(model.SelectedPriceRange);
            var filterStrategies = CarFilterStrategyFactory
                .GetStrategies(model.SelectedYear, priceRanges[0], priceRanges[1], WebConstants.AllFilterTypeModelValue);
            var mutipleFilterStrategy = CarMultipleFilterStrategyFactory.GetStrategy(new string[] { model.SelectedModelType });

            var filteredCars = this.carsService.GetFiltered<BaseCar>(filterStrategies.ToArray());
            filteredCars = mutipleFilterStrategy.Filter(filteredCars);

            var searchModel = await this.homeService.GetSearchModelAsync(filteredCars, model.SelectedCarType);

            _ = this.cacheService.AddInfinityCacheAsync(searchModel, cacheKey, WebConstants.CacheCarsType);

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

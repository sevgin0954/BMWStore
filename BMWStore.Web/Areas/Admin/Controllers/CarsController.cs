using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarsController : BaseAdminController
    {
        private readonly IAdminCarsService adminCarsService;
        private readonly IAdminSortCookieService adminSortCookieService;
        private readonly IEnginesService enginesService;
        private readonly IFuelTypesService fuelTypesService;
        private readonly IModelTypesService modelTypesService;
        private readonly ISeriesService seriesService;

        public CarsController(
            IAdminCarsService adminCarsService, 
            IAdminSortCookieService adminSortCookieService, 
            IEnginesService enginesService,
            IFuelTypesService fuelTypesService,
            IModelTypesService modelTypesService,
            ISeriesService seriesService)
        {
            this.adminCarsService = adminCarsService;
            this.adminSortCookieService = adminSortCookieService;
            this.enginesService = enginesService;
            this.fuelTypesService = fuelTypesService;
            this.modelTypesService = modelTypesService;
            this.seriesService = seriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cars = await this.adminCarsService.GetAllCarsAsync();
            var cookies = this.HttpContext.Request.Cookies;
            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            var sortStrategyType = this.adminSortCookieService
                .GetSortStrategyTypeOrDefault<CarSortStrategyType>(cookies, sortTypeKey);
            var model = new AdminCarsViewModel()
            {
                Cars = cars,
                SortStrategyDirection = this.adminSortCookieService
                    .GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey),
                SortStrategyType = sortStrategyType
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var engines = await this.enginesService.GetAllAsSelectListItemsAsync();
            var fuelTypes = await this.fuelTypesService.GetAllAsSelectListItemsAsync();
            var modelTypes = await this.modelTypesService.GetAllAsSelectListItemsAsync();
            var series = await this.seriesService.GetAllAsSelectListItemsAsync();
            var model = new AdminCarCreateBindingModel()
            {
                Engines = engines,
                FuelTypes = fuelTypes,
                ModelTypes = modelTypes,
                Series = series
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminCarCreateBindingModel model)
        {
            await this.adminCarsService.CreateNewCar(model);

            return Redirect(WebConstants.AdminCarsUrl);
        }
    }
}
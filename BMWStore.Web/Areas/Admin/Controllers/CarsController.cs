using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarsController : BaseAdminController
    {
        private readonly IAdminCarsService adminCarsService;
        private readonly IAdminSortCookieService adminSortCookieService;
        private readonly IAdminEnginesService adminEnginesService;
        private readonly IAdminFuelTypesService adminFuelTypesService;
        private readonly IAdminModelTypesService adminModelTypesService;
        private readonly IAdminSeriesService adminSeriesService;
        private readonly IAdminCarOptionsService adminCarOptionsService;

        public CarsController(
            IAdminCarsService adminCarsService, 
            IAdminSortCookieService adminSortCookieService, 
            IAdminEnginesService enginesService,
            IAdminFuelTypesService fuelTypesService,
            IAdminModelTypesService modelTypesService,
            IAdminSeriesService seriesService,
            IAdminCarOptionsService adminCarOptionsService)
        {
            this.adminCarsService = adminCarsService;
            this.adminSortCookieService = adminSortCookieService;
            this.adminEnginesService = enginesService;
            this.adminFuelTypesService = fuelTypesService;
            this.adminModelTypesService = modelTypesService;
            this.adminSeriesService = seriesService;
            this.adminCarOptionsService = adminCarOptionsService;
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
            var engines = await this.adminEnginesService.GetAllAsSelectListItemsAsync();
            var fuelTypes = await this.adminFuelTypesService.GetAllAsSelectListItemsAsync();
            var modelTypes = await this.adminModelTypesService.GetAllAsSelectListItemsAsync();
            var series = await this.adminSeriesService.GetAllAsSelectListItemsAsync();
            var options = await this.adminCarOptionsService.GetAllAsSelectListItemsAsync();
            var model = new AdminNewCarCreateBindingModel()
            {
                Engines = engines,
                FuelTypes = fuelTypes,
                ModelTypes = modelTypes,
                Series = series,
                CarOptions = options
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminNewCarCreateBindingModel model)
        {
            await this.adminCarsService.CreateNewCar(model);

            return Redirect(WebConstants.AdminCarsUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminCarsService.DeleteCarAsync(id);

            return Redirect(WebConstants.AdminCarsUrl);
        }
    }
}
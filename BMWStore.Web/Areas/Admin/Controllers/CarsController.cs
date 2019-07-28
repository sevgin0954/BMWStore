using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarsController : BaseAdminController
    {
        private readonly IAdminCarsService adminCarsService;
        private readonly ISortCookieService sortCookieService;
        private readonly IAdminEnginesService enginesService;
        private readonly IAdminFuelTypesService fuelTypesService;
        private readonly IAdminModelTypesService modelTypesService;
        private readonly IAdminSeriesService seriesService;
        private readonly IAdminOptionsService carOptionsService;
        private readonly ICarsService carsService;

        public CarsController(
            IAdminCarsService adminCarsService, 
            ISortCookieService sortCookieService, 
            IAdminEnginesService enginesService,
            IAdminFuelTypesService fuelTypesService,
            IAdminModelTypesService modelTypesService,
            IAdminSeriesService seriesService,
            IAdminOptionsService adminCarOptionsService,
            ICarsService carsService)
        {
            this.adminCarsService = adminCarsService;
            this.sortCookieService = sortCookieService;
            this.enginesService = enginesService;
            this.fuelTypesService = fuelTypesService;
            this.modelTypesService = modelTypesService;
            this.seriesService = seriesService;
            this.carOptionsService = adminCarOptionsService;
            this.carsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<AdminBaseCarSortStrategyType>(cookies, sortTypeKey);

            var sortStrategy = BaseCarSortStrategyFactory.GetStrategy<BaseCar>(sortType, sortDirection);
            var cars = await this.carsService.GetAllCarsAsync(sortStrategy);
            var model = new AdminCarsViewModel()
            {
                Cars = cars,
                SortStrategyDirection = this.sortCookieService
                    .GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey),
                SortStrategyType = sortType
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
            var options = await this.carOptionsService.GetAllAsSelectListItemsAsync();
            var model = new AdminCarCreateBindingModel()
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
        public async Task<IActionResult> AddNew(AdminCarCreateBindingModel model)
        {
            if (model.Mileage > 0)
            {
                await this.adminCarsService.CreateCarAsync<UsedCar>(model);
            }
            else
            {
                await this.adminCarsService.CreateCarAsync<NewCar>(model);
            }

            return Redirect(WebConstants.AdminCarsUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminCarsService.DeleteCarAsync(id);

            return Redirect(WebConstants.AdminCarsUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var engines = await this.enginesService.GetAllAsSelectListItemsAsync();
            var fuelTypes = await this.fuelTypesService.GetAllAsSelectListItemsAsync();
            var modelTypes = await this.modelTypesService.GetAllAsSelectListItemsAsync();
            var series = await this.seriesService.GetAllAsSelectListItemsAsync();
            var options = await this.carOptionsService.GetAllAsSelectListItemsAsync();
            var model = new AdminCarEditBindingModel()
            {
                Id = id,
                Engines = engines,
                FuelTypes = fuelTypes,
                ModelTypes = modelTypes,
                Series = series,
                CarOptions = options
            };
            await this.adminCarsService.SetEditBindingModelPropertiesAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminCarEditBindingModel model)
        {
            if (model.IsNew)
            {
                await this.adminCarsService.EditNewCarAsync(model);
            }
            else
            {
                await this.adminCarsService.EditUsedCarAsync(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(AdminBaseCarSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            this.sortCookieService.ChangeSortTypeCookie(this.HttpContext.Response.Cookies, sortStrategyType, sortTypeKey);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            this.sortCookieService.ChangeSortDirectionCookie(this.HttpContext.Response.Cookies, sortDirection, sortDirectionKey);

            return RedirectToAction("Index");
        }
    }
}
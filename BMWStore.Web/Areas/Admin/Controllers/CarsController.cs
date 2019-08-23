using BMWStore.Common.Constants;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Entities;
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
        private readonly ICookiesService cookiesService;
        private readonly ISelectListItemsService selectListItemsService;
        private readonly ICacheService cacheService;

        public CarsController(
            IAdminCarsService adminCarsService,
            ICookiesService cookiesService,
            ISelectListItemsService selectListItemsService,
            ICacheService cacheService)
        {
            this.adminCarsService = adminCarsService;
            this.cookiesService = cookiesService;
            this.selectListItemsService = selectListItemsService;
            this.cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            AdminBaseCarFilterStrategy filter = AdminBaseCarFilterStrategy.All,
            string name = "",
            int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            var sortType = this.cookiesService.GetValueOrDefault<AdminBaseCarSortStrategyType>(cookies, sortTypeKey);

            var filterStrategy = AdminCarFilterStrategyFactory.GetStrategy(filter, name);
            var model = await this.adminCarsService.GetCarsViewModelAsync(filterStrategy, sortDirection, sortType, pageNumber);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var engines = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Engine>();
            var fuelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<FuelType>();
            var modelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<ModelType>();
            var series = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Series>();
            var options = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Option>();
            var model = new AdminCarBindingModel()
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
        public async Task<IActionResult> AddNew(AdminCarBindingModel model)
        {
            if (model.Mileage > 0)
            {
                await this.adminCarsService.CreateCarAsync<UsedCar>(model);
            }
            else
            {
                await this.adminCarsService.CreateCarAsync<NewCar>(model);
            }

            await this.cacheService.RemoveAsync(WebConstants.CacheCarsType);

            return Redirect(WebConstants.AdminCarsUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminCarsService.DeleteAsync(id);

            await this.cacheService.RemoveAsync(WebConstants.CacheCarsType);

            return Redirect(WebConstants.AdminCarsUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var engines = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Engine>();
            var fuelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<FuelType>();
            var modelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<ModelType>();
            var series = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Series>();
            var options = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Option>();
            var model = new AdminCarBindingModel()
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
        public async Task<IActionResult> Edit(AdminCarBindingModel model)
        {
            if (model.IsNew)
            {
                await this.adminCarsService.EditCarAsync<NewCar>(model);
            }
            else
            {
                await this.adminCarsService.EditCarAsync<UsedCar>(model);
            }

            await this.cacheService.RemoveAsync(WebConstants.CacheCarsType);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(AdminBaseCarSortStrategyType sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}
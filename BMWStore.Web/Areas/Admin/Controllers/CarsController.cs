using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
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
        private readonly ISortCookieService sortCookieService;
        private readonly ISelectListItemsService selectListItemsService;

        public CarsController(
            IAdminCarsService adminCarsService, 
            ISortCookieService sortCookieService,
            ISelectListItemsService selectListItemsService)
        {
            this.adminCarsService = adminCarsService;
            this.sortCookieService = sortCookieService;
            this.selectListItemsService = selectListItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string id,
            AdminBaseCarFilterStrategy filter = AdminBaseCarFilterStrategy.All,
            int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortDirectionKey = WebConstants.CookieAdminCarsSortDirectionKey;
            var sortDirection = this.sortCookieService.GetSortStrategyDirectionOrDefault(cookies, sortDirectionKey);

            var sortTypeKey = WebConstants.CookieAdminCarsSortTypeKey;
            var sortType = this.sortCookieService.GetSortStrategyTypeOrDefault<AdminBaseCarSortStrategyType>(cookies, sortTypeKey);

            var model = await this.adminCarsService.GetCarsViewModelAsync(id, sortDirection, sortType, filter, pageNumber);

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
            var engines = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Engine>();
            var fuelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<FuelType>();
            var modelTypes = await this.selectListItemsService.GetAllAsSelectListItemsAsync<ModelType>();
            var series = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Series>();
            var options = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Option>();
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
                await this.adminCarsService.EditCarAsync<NewCar>(model);
            }
            else
            {
                await this.adminCarsService.EditCarAsync<UsedCar>(model);
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
using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.FilterStrategies;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.FilterStrategyFactory;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Entities;
using BMWStore.Exceptions.Repositories;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.CarModels.BindingModels;
using BMWStore.Models.CarModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarsController : BaseAdminController
    {
        private readonly IAdminCarsService adminCarsService;
        private readonly ICookiesService cookiesService;
        private readonly ICacheService cacheService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ICarsService carsService;
        private readonly IAdminEnginesService adminEnginesService;
        private readonly IAdminFuelTypesService adminFuelTypesService;
        private readonly IAdminModelTypesService adminModelTypesService;
        private readonly IAdminSeriesService adminSeriesService;
        private readonly IAdminOptionsService adminOptionsService;

        public CarsController(
            IAdminCarsService adminCarsService,
            ICookiesService cookiesService,
            ICacheService cacheService,
            ICloudinaryService cloudinaryService,
            ICarsService carsService,
            IAdminEnginesService adminEnginesService,
            IAdminFuelTypesService adminFuelTypesService,
            IAdminModelTypesService adminModelTypesService,
            IAdminSeriesService adminSeriesService,
            IAdminOptionsService adminOptionsService)
        {
            this.adminCarsService = adminCarsService;
            this.cookiesService = cookiesService;
            this.cacheService = cacheService;
            this.cloudinaryService = cloudinaryService;
            this.carsService = carsService;
            this.adminEnginesService = adminEnginesService;
            this.adminFuelTypesService = adminFuelTypesService;
            this.adminModelTypesService = adminModelTypesService;
            this.adminSeriesService = adminSeriesService;
            this.adminOptionsService = adminOptionsService;
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
            var sortStrategy = AdminBaseCarSortStrategyFactory.GetStrategy<BaseCar>(sortType, sortDirection);

            var carServiceModels = this.carsService.GetCars(sortStrategy, filterStrategy);
            var carServiceModelsFromCurrentPage = carServiceModels.GetFromPage(pageNumber);
            var carViewModels = Mapper.Map<IEnumerable<CarConciseViewModel>>(carServiceModelsFromCurrentPage);
            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(carServiceModels);
            var model = new AdminCarsViewModel()
            {
                Cars = carViewModels,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var engines = await this.adminEnginesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var fuelTypes = await this.adminFuelTypesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var modelTypes = await this.adminModelTypesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var series = await this.adminSeriesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var options = await this.adminOptionsService.GetAll().To<SelectListItem>().ToArrayAsync();
            var model = new AdminCarBindingModel()
            {
                Engines = engines,
                FuelTypes = fuelTypes,
                ModelTypes = modelTypes,
                Series = series,
                Options = options
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminCarBindingModel model)
        {
            var serviceModel = Mapper.Map<CarServiceModel>(model);
            await this.AddPicturesToServiceModel(serviceModel, model);

            if (model.Mileage > 0)
            {
                await this.adminCarsService.CreateNewAsync<UsedCar>(serviceModel);
            }
            else
            {
                await this.adminCarsService.CreateNewAsync<NewCar>(serviceModel);
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
            // TODO: Cache that
            var engines = await this.adminEnginesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var fuelTypes = await this.adminFuelTypesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var modelTypes = await this.adminModelTypesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var series = await this.adminSeriesService.GetAll().To<SelectListItem>().ToArrayAsync();
            var options = await this.adminOptionsService.GetAll().To<SelectListItem>().ToArrayAsync();
            var carServiceModel = await this.carsService.GetByIdAsync(id);
            var carBindingModel = Mapper.Map<AdminCarBindingModel>(carServiceModel);

            SelectListItemHelper.SelectItemsWithValues(engines, carServiceModel.EngineId);
            SelectListItemHelper.SelectItemsWithValues(fuelTypes, carServiceModel.FuelTypeId);
            SelectListItemHelper.SelectItemsWithValues(modelTypes, carServiceModel.ModelTypeId);
            SelectListItemHelper.SelectItemsWithValues(series, carServiceModel.SeriesId);
            SelectListItemHelper.SelectItemsWithValues(options, carServiceModel.Options.Select(o => o.OptionId).ToArray());

            carBindingModel.Engines = engines;
            carBindingModel.FuelTypes = fuelTypes;
            carBindingModel.ModelTypes = modelTypes;
            carBindingModel.Series = series;
            carBindingModel.Options = options;

            return View(carBindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminCarBindingModel model)
        {
            const string redirectLocation = "Index";

            var serviceModel = Mapper.Map<CarServiceModel>(model);
            await this.AddPicturesToServiceModel(serviceModel, model);

            try
            {
                if (model.IsNew)
                {
                    await this.adminCarsService.EditAsync<NewCar>(serviceModel);
                }
                else
                {
                    await this.adminCarsService.EditAsync<UsedCar>(serviceModel);
                }
            }
            catch (RepositoryUpdateNoRowsAffectedException)
            {
                return RedirectToAction(redirectLocation);
            }

            await this.cacheService.RemoveAsync(WebConstants.CacheCarsType);

            return RedirectToAction(redirectLocation);
        }

        private async Task AddPicturesToServiceModel(CarServiceModel serviceModel, AdminCarBindingModel model)
        {
            var publicIds = await this.cloudinaryService.UploadPicturesAsync(model.Pictures);
            serviceModel.Pictures = Mapper.Map<ICollection<PictureServiceModel>>(publicIds);
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
using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Models.EngineModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class EnginesController : BaseAdminController
    {
        private readonly IAdminEnginesService enginesService;
        private readonly ICookiesService cookiesService;
        private readonly IEngineRepository engineRepository;
        private readonly IAdminTransmissionsService adminTransmissionsService;

        public EnginesController(
            IAdminEnginesService enginesService,
            ICookiesService cookiesService,
            IEngineRepository engineRepository,
            IAdminTransmissionsService adminTransmissionsService)
        {
            this.enginesService = enginesService;
            this.cookiesService = cookiesService;
            this.engineRepository = engineRepository;
            this.adminTransmissionsService = adminTransmissionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminEngineSortTypeKey;
            var sortDirectionKey = WebConstants.CookieAdminEngineSortDirectionKey;

            var sortType = this.cookiesService.GetValueOrDefault<EngineSortStrategy>(cookies, sortTypeKey);
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);
            var sortStrategy = EnginesSortStrategyFactory.GetStrategy(sortType, sortDirection);
            var sortedEngines = this.enginesService.GetSorted(sortStrategy, pageNumber);
            var engineModels = await sortedEngines.To<EngineViewModel>().ToArrayAsync();

            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(this.engineRepository.GetAll());
            var model = new AdminEnginesViewModel()
            {
                Engines = engineModels,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount,
                SortStrategyDirection = sortDirection,
                SortStrategyType = sortType
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var allTransmissions = await this.adminTransmissionsService
                .GetAll()
                .To<SelectListItem>()
                .ToArrayAsync();
            var model = new EngineBindingModel()
            {
                Transmissions = allTransmissions
            };

            // TODO: Fix redirect
            //this.ViewData[WebConstants.ReturnControllerName] = "Engines";
            //this.ViewData[WebConstants.ReturnActionName] = "Index";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(EngineBindingModel model)
        {
            var serviceModel = Mapper.Map<EngineServiceModel>(model);
            await this.enginesService.CreateNewAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var bindingModel = await this.enginesService.GetEngineByIdAsync<EngineBindingModel>(id);

            var allTransmissions = await this.adminTransmissionsService.GetAll().To<SelectListItem>().ToArrayAsync();

            var selectedTransmissionsId = bindingModel.Transmissions.Select(t => t.Value).First();
            SelectListItemHelper.SelectItemsWithValues(allTransmissions, selectedTransmissionsId);

            bindingModel.Transmissions = allTransmissions;

            return View(bindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EngineBindingModel model)
        {
            var serviceModel = Mapper.Map<EngineServiceModel>(model);
            await this.enginesService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.enginesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortType(EngineSortStrategy sortStrategyType)
        {
            var sortTypeKey = WebConstants.CookieAdminEngineSortTypeKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortTypeKey, sortStrategyType.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeSortDirection(SortStrategyDirection sortDirection)
        {
            var sortDirectionKey = WebConstants.CookieAdminEngineSortDirectionKey;
            this.cookiesService.SetCookieValue(this.HttpContext.Response.Cookies, sortDirectionKey, sortDirection.ToString());

            return RedirectToAction("Index");
        }
    }
}
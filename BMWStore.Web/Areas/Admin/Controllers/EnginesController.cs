using BMWStore.Common.Constants;
using BMWStore.Common.Enums.SortStrategies;
using BMWStore.Data.Factories.SortStrategyFactories;
using BMWStore.Entities;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class EnginesController : BaseAdminController
    {
        private readonly IAdminEnginesService enginesService;
        private readonly ISelectListItemsService selectListItemsService;
        private readonly ICookiesService cookiesService;

        public EnginesController(
            IAdminEnginesService enginesService, 
            ISelectListItemsService selectListItemsService,
            ICookiesService cookiesService)
        {
            this.enginesService = enginesService;
            this.selectListItemsService = selectListItemsService;
            this.cookiesService = cookiesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var cookies = this.HttpContext.Request.Cookies;

            var sortTypeKey = WebConstants.CookieAdminEngineSortTypeKey;
            var sortDirectionKey = WebConstants.CookieAdminEngineSortDirectionKey;

            var engineSortStrategy = this.cookiesService.GetValueOrDefault<EngineSortStrategy>(cookies, sortTypeKey);
            var sortDirection = this.cookiesService.GetValueOrDefault<SortStrategyDirection>(cookies, sortDirectionKey);
            var sortStrategy = EnginesSortStrategyFactory.GetStrategy(engineSortStrategy, sortDirection);
            var model = await this.enginesService.GetEnginesViewModelAsync(pageNumber, sortStrategy);

            model.SortStrategyDirection = sortDirection;
            model.SortStrategyType = engineSortStrategy;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var allTransmissions = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Transmission>();
            var model = new AdminEngineCreateBindingModel()
            {
                Transmissions = allTransmissions
            };

            this.ViewData[WebConstants.ReturnControllerName] = "Engines";
            this.ViewData[WebConstants.ReturnActionName] = "Index";

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminEngineCreateBindingModel model)
        {
            await this.enginesService.CreateEngineAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.enginesService.GetEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEngineEditBindingModel model)
        {
            await this.enginesService.EditAsync(model);

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
using BMWStore.Common.Constants;
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

        public EnginesController(IAdminEnginesService enginesService, ISelectListItemsService selectListItemsService)
        {
            this.enginesService = enginesService;
            this.selectListItemsService = selectListItemsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.enginesService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var transmissions = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Transmission>();
            var model = new AdminEngineCreateBindingModel()
            {
                Transmissions = transmissions
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
            var transmissions = await this.selectListItemsService.GetAllAsSelectListItemsAsync<Transmission>();
            var model = new AdminEngineEditBindingModel()
            {
                Id = id,
                Transmissions = transmissions
            };
            await this.enginesService.SetEditBindingModelPropertiesAsync(model);

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
    }
}
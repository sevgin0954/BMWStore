using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionTypesController : BaseAdminController
    {
        private readonly IAdminOptionTypesService adminOptionTypesService;

        public OptionTypesController(IAdminOptionTypesService adminOptionTypesService)
        {
            this.adminOptionTypesService = adminOptionTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.adminOptionTypesService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new OptionTypeBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionTypeBindingModel model)
        {
            await this.adminOptionTypesService.CreateOptionTypeAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.adminOptionTypesService.GetEditingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OptionTypeBindingModel model)
        {
            await this.adminOptionTypesService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminOptionTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
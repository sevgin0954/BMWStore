using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class ModelTypesController : BaseAdminController
    {
        private readonly IAdminModelTypesService modelTypesService;

        public ModelTypesController(IAdminModelTypesService modelTypesService)
        {
            this.modelTypesService = modelTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.modelTypesService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new ModelTypeCreateBidningModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(ModelTypeCreateBidningModel model)
        {
            await this.modelTypesService.CreateNewModelType(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.modelTypesService.GetEditingModel(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ModelTypeEditBindingModel model)
        {
            await this.modelTypesService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.modelTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
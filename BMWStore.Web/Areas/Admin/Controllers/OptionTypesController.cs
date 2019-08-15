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
            var model = new OptionTypeCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionTypeCreateBindingModel model)
        {
            await this.adminOptionTypesService.CreateOptionTypeAsync(model);

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
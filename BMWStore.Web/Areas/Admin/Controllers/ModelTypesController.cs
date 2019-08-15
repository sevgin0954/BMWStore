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
            var model = new AdminModelTypeCreateBidningModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminModelTypeCreateBidningModel model)
        {
            await this.modelTypesService.CreateNewModelType(model);

            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.modelTypesService.DeleteAsync(id);

            return Redirect("Index");
        }
    }
}
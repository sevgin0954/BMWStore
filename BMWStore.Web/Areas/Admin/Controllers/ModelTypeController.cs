using BMWStore.Common.Constants;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class ModelTypeController : BaseAdminController
    {
        private readonly IModelTypesService modelTypesService;

        public ModelTypeController(IModelTypesService modelTypesService)
        {
            this.modelTypesService = modelTypesService;
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

            return Redirect(WebConstants.AdminCreateNewCarUrl);
        }
    }
}
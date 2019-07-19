using BMWStore.Common.Constants;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class ModelTypeController : BaseAdminController
    {
        private readonly IAdminModelTypesService modelTypesService;

        public ModelTypeController(IAdminModelTypesService modelTypesService)
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
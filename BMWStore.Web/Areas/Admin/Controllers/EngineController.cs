using BMWStore.Common.Constants;
using BMWStore.Models.EngineModels.BindingModels;
using BMWStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class EngineController : BaseAdminController
    {
        private readonly ITransmissionsService transmissionsService;
        private readonly IEnginesService enginesService;

        public EngineController(ITransmissionsService transmissionsService, IEnginesService enginesService)
        {
            this.transmissionsService = transmissionsService;
            this.enginesService = enginesService;
        }

        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            var transmissions = await this.transmissionsService.GetAllAsSelectListItemsAsync();
            var model = new AdminEngineCreateBindingModel()
            {
                Transmissions = transmissions
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminEngineCreateBindingModel model)
        {
            await this.enginesService.CreateNewEngineAsync(model);

            return Redirect(WebConstants.AdminCreateNewCarUrl);
        }
    }
}
using BMWStore.Models.OptionModels.BidningModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class CarOptionsController : BaseAdminController
    {
        private readonly IAdminCarOptionsService adminCarOptionsService;

        public CarOptionsController(IAdminCarOptionsService adminCarOptionsService)
        {
            this.adminCarOptionsService = adminCarOptionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.adminCarOptionsService.GetAllOptionsAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new AdminOptionCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminOptionCreateBindingModel model)
        {
            await this.adminCarOptionsService.CreateNewCarOptionAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminCarOptionsService.DeleteCarOptionAsync(id);

            return RedirectToAction("Index");
        }
    }
}
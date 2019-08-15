using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class SeriesController : BaseAdminController
    {
        private readonly IAdminSeriesService seriesService;

        public SeriesController(IAdminSeriesService seriesService)
        {
            this.seriesService = seriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.seriesService.GetAllAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new SeriesCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(SeriesCreateBindingModel model)
        {
            await this.seriesService.CreateNewSeriesAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.seriesService.GetEditingModelAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeriesEditBindingModel model)
        {
            await this.seriesService.EditAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.seriesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
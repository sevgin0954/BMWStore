using BMWStore.Entities;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class SeriesController : BaseAdminController
    {
        private readonly IAdminSeriesService seriesService;
        private readonly IAdminDeleteService adminDeleteService;

        public SeriesController(IAdminSeriesService seriesService, IAdminDeleteService adminDeleteService)
        {
            this.seriesService = seriesService;
            this.adminDeleteService = adminDeleteService;
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
            var model = new AdminSeriesCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminSeriesCreateBindingModel model)
        {
            await this.seriesService.CreateNewSeriesAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminDeleteService.DeleteAsync<Series>(id);

            return RedirectToAction("Index");
        }
    }
}
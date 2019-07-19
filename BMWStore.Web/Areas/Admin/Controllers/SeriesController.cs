using BMWStore.Common.Constants;
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
        public IActionResult AddNew()
        {
            var model = new AdminSeriesCreateBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(AdminSeriesCreateBindingModel model)
        {
            await this.seriesService.CreateNewSeriesAsync(model);

            return Redirect(WebConstants.AdminCreateNewCarUrl);
        }
    }
}
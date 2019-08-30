using AutoMapper;
using BMWStore.Models.SeriesModels.BindingModels;
using BMWStore.Models.SeriesModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var viewModels = await this.seriesService.GetAll()
                .To<SeriesViewModel>()
                .ToArrayAsync();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new SeriesBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(SeriesBindingModel model)
        {
            var serviceModel = Mapper.Map<SeriesServiceModel>(model);
            await this.seriesService.CreateNewAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var serviceModel = await this.seriesService.GetByIdAsync(id);
            var editingModel = Mapper.Map<SeriesBindingModel>(serviceModel);

            return View(editingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SeriesBindingModel model)
        {
            var serviceModel = Mapper.Map<SeriesServiceModel>(model);
            await this.seriesService.EditAsync(serviceModel);

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
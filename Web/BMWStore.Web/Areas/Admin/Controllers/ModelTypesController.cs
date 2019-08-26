using AutoMapper;
using BMWStore.Models.ModelTypeModels.BindingModels;
using BMWStore.Models.ModelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var models = await this.modelTypesService.GetAll()
                .To<ModelTypeViewModel>()
                .ToArrayAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new ModelTypeBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(ModelTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<ModelTypeServiceModel>(model);
            await this.modelTypesService.CreateNewAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.modelTypesService.GetByIdAsync<ModelTypeBindingModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ModelTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<ModelTypeServiceModel>(model);
            await this.modelTypesService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.modelTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
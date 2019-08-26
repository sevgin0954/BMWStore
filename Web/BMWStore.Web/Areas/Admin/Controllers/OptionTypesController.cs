using AutoMapper;
using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Models.OptionTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class OptionTypesController : BaseAdminController
    {
        private readonly IAdminOptionTypesService adminOptionTypesService;

        public OptionTypesController(IAdminOptionTypesService adminOptionTypesService)
        {
            this.adminOptionTypesService = adminOptionTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var models = await this.adminOptionTypesService.GetAll()
                .To<OptionTypeConciseViewModel>()
                .ToArrayAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new OptionTypeBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(OptionTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<OptionTypeServiceModel>(model);
            await this.adminOptionTypesService.CreateNewAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await this.adminOptionTypesService.GetByIdAsync<OptionTypeBindingModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OptionTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<OptionTypeServiceModel>(model);
            await this.adminOptionTypesService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.adminOptionTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Models.TransmissionsModels.BindingModels;
using BMWStore.Models.TransmissionsModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class TransmissionController : BaseAdminController
    {
        private readonly IAdminTransmissionsService transmissionsService;

        public TransmissionController(IAdminTransmissionsService transmissionsService)
        {
            this.transmissionsService = transmissionsService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await this.transmissionsService.GetAll()
                .To<TransmissionViewModel>()
                .ToArrayAsync();

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new TransmissionBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(TransmissionBindingModel model)
        {
            var serviceModel = Mapper.Map<TransmissionServiceModel>(model);
            await this.transmissionsService.CreateNewAsync(serviceModel);

            return Redirect(WebConstants.AdminCreateNewEngineUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var serviceModel = await this.transmissionsService.GetByIdAsync(id);
            var bindingModel = Mapper.Map<TransmissionBindingModel>(serviceModel);

            return View(bindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TransmissionBindingModel model)
        {
            var serviceModel = Mapper.Map<TransmissionServiceModel>(model);
            await this.transmissionsService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.transmissionsService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
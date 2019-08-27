using AutoMapper;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Helpers;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Models.FuelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Models;
using MappingRegistrar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BMWStore.Web.Areas.Admin.Controllers
{
    public class FuelTypesController : BaseAdminController
    {
        private readonly IAdminFuelTypesService fuelTypesService;
        private readonly IFuelTypeRepository fuelTypeRepository;

        public FuelTypesController(IAdminFuelTypesService fuelTypesService, IFuelTypeRepository fuelTypeRepository)
        {
            this.fuelTypesService = fuelTypesService;
            this.fuelTypeRepository = fuelTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var serviceModels = this.fuelTypesService.GetAll(pageNumber);
            var fuelTypeModels = await serviceModels.To<FuelTypeViewModel>().ToArrayAsync();
            var totalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(this.fuelTypeRepository.GetAll());
            var model = new AdminFuelTypesViewModel()
            {
                FuelTypes = fuelTypeModels,
                CurrentPage = pageNumber,
                TotalPagesCount = totalPagesCount
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            var model = new FuelTypeBindingModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNew(FuelTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<FuelTypeServiceModel>(model);
            await this.fuelTypesService.CreateNewAsync(serviceModel);

            return Redirect("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var serviceModel = await this.fuelTypesService.GetByIdAsync(id);
            var bindingModel = Mapper.Map<FuelTypeBindingModel>(serviceModel);

            return View(bindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FuelTypeBindingModel model)
        {
            var serviceModel = Mapper.Map<FuelTypeServiceModel>(model);
            await this.fuelTypesService.EditAsync(serviceModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.fuelTypesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
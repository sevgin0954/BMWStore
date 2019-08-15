using AutoMapper;
using BMWStore.Common.Helpers;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.AdminModels.ViewModels;
using BMWStore.Models.FuelTypeModels.BindingModels;
using BMWStore.Models.FuelTypeModels.ViewModels;
using BMWStore.Services.AdminServices.Interfaces;
using BMWStore.Services.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminFuelTypesService : IAdminFuelTypesService
    {
        private readonly IFuelTypeRepository fuelTypeRepository;
        private readonly IReadService readService;
        private readonly IAdminDeleteService adminDeleteService;
        private readonly IAdminEditService adminEditService;

        public AdminFuelTypesService(
            IFuelTypeRepository fuelTypeRepository, 
            IReadService readService,
            IAdminDeleteService adminDeleteService,
            IAdminEditService adminEditService)
        {
            this.fuelTypeRepository = fuelTypeRepository;
            this.readService = readService;
            this.adminDeleteService = adminDeleteService;
            this.adminEditService = adminEditService;
        }

        public async Task CreateNewFuelTypeAsync(FuelTypeCreateBindingModel model)
        {
            var dbFuelType = Mapper.Map<FuelType>(model);
            this.fuelTypeRepository.Add(dbFuelType);

            var rowsAffected = await this.fuelTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }

        public async Task<AdminFuelTypesViewModel> GetFuelTypesViewModelAsync(int pageNumber)
        {
            var fuelTypeModels = await this.readService.GetAllAsync<FuelTypeViewModel, FuelType>(pageNumber);
            var model = new AdminFuelTypesViewModel()
            {
                FuelTypes = fuelTypeModels,
                CurrentPage = pageNumber,
                TotalPagesCount = await PaginationHelper.CountTotalPagesCountAsync(this.fuelTypeRepository.GetAll())
            };

            return model;
        }

        public async Task<FuelTypeEditBindingModel> GetEditingModelAsync(string fuelTypeId)
        {
            var model = await this.readService.GetModelByIdAsync<FuelTypeEditBindingModel, FuelType>(fuelTypeId);

            return model;
        }

        public async Task EditAsync(FuelTypeEditBindingModel model)
        {
            await this.adminEditService.EditAsync<FuelType, FuelTypeEditBindingModel>(model, model.Id);
        }

        public async Task DeleteAsync(string fuelTypeId)
        {
            await this.adminDeleteService.DeleteAsync<FuelType>(fuelTypeId);
        }
    }
}

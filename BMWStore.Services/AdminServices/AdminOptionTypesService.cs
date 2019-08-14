using AutoMapper;
using BMWStore.Common.Validation;
using BMWStore.Data.Repositories.Interfaces;
using BMWStore.Entities;
using BMWStore.Models.OptionTypeModels.BindingModels;
using BMWStore.Services.AdminServices.Interfaces;
using System.Threading.Tasks;

namespace BMWStore.Services.AdminServices
{
    public class AdminOptionTypesService : IAdminOptionTypesService
    {
        private readonly IOptionTypeRepository optionTypeRepository;

        public AdminOptionTypesService(IOptionTypeRepository optionTypeRepository)
        {
            this.optionTypeRepository = optionTypeRepository;
        }

        public async Task CreateOptionTypeAsync(OptionTypeCreateBindingModel model)
        {
            var dbOptionType = Mapper.Map<OptionType>(model);
            this.optionTypeRepository.Add(dbOptionType);

            var rowsAffected = await this.optionTypeRepository.CompleteAsync();
            UnitOfWorkValidator.ValidateUnitOfWorkCompleteChanges(rowsAffected);
        }
    }
}

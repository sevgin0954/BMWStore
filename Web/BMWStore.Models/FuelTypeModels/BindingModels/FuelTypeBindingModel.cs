using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.FuelTypeModels.BindingModels
{
    public class FuelTypeBindingModel : IMapTo<FuelType>, IMapFrom<FuelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.FuelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.FuelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FuelTypeBindingModel, FuelType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

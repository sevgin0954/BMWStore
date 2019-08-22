using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.ModelTypeModels.BindingModels
{
    public class ModelTypeBindingModel : IMapTo<ModelType>, IMapFrom<ModelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.ModelTypeNameMaxLength)]
        [MinLength(EntitiesConstants.ModelTypeNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ModelTypeBindingModel, ModelType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
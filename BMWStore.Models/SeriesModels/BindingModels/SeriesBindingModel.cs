using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.SeriesModels.BindingModels
{
    public class SeriesBindingModel : IMapFrom<Series>, IMapTo<Series>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [MaxLength(EntitiesConstants.SeriesNameMaxLength)]
        [MinLength(EntitiesConstants.SeriesNameMinLength)]
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SeriesBindingModel, Series>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

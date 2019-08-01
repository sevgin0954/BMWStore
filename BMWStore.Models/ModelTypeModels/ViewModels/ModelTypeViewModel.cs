using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.ModelTypeModels.ViewModels
{
    public class ModelTypeViewModel : IMapFrom<ModelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ModelType, ModelTypeViewModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count));
        }
    }
}

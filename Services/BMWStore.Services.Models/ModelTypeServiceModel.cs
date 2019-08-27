using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class ModelTypeServiceModel : IMapFrom<ModelType>, IMapTo<ModelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ModelTypeServiceModel, ModelType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            configuration.CreateMap<ModelType, ModelTypeServiceModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count()));
        }
    }
}

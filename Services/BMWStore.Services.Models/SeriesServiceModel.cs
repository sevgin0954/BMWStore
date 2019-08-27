using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Linq;

namespace BMWStore.Services.Models
{
    public class SeriesServiceModel : IMapFrom<Series>, IMapTo<Series>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Series, SeriesServiceModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count()));

            configuration.CreateMap<SeriesServiceModel, Series>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

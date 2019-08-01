using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.SeriesModels.ViewModels
{
    public class SeriesViewModel : IMapFrom<Series>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public int CarsCount { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Series, SeriesViewModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(dest => dest.Cars.Count));
        }
    }
}

using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.OptionModels.ViewModels
{
    public class OptionViewModel : IMapFrom<Option>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CarsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Option, OptionViewModel>()
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(dest => dest.CarsOptions.Count));
        }
    }
}

using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.EngineModels.ViewModels
{
    public class EngineViewModel : IMapFrom<Engine>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TransmissionName { get; set; }

        public decimal Price { get; set; }

        public int Weight_Kg { get; set; }

        public int CarsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Engine, EngineViewModel>()
                .ForMember(dest => dest.TransmissionName, opt => opt.MapFrom(src => src.Transmission.Name))
                .ForMember(dest => dest.CarsCount, opt => opt.MapFrom(src => src.Cars.Count));
        }
    }
}

using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class FuelTypeServiceModel : IMapFrom<FuelType>, IMapTo<FuelType>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ICollection<CarServiceModel> Cars { get; set; } = new List<CarServiceModel>();

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<FuelTypeServiceModel, FuelType>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}

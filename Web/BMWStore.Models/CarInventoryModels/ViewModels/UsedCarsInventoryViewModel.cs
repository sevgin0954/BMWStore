using AutoMapper;
using BMWStore.Common.Enums.SortStrategies;
using MappingRegistrar.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BMWStore.Models.CarInventoryModels.ViewModels
{
    public class UsedCarsInventoryViewModel : CarsInventoryViewModel, IMapTo<CarsInventoryViewModel>, IHaveCustomMappings
    {
        [JsonProperty("SortStrategyType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public UsedCarSortStrategyType BaseSortStrategyType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UsedCarsInventoryViewModel, CarsInventoryViewModel>()
                .ForMember(dest => dest.SortStrategyType, opt => opt.MapFrom(src => src.BaseSortStrategyType));
        }
    }
}

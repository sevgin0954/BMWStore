using AutoMapper;
using BMWStore.Common.Enums.SortStrategies;
using MappingRegistrar.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BMWStore.Models.CarInventoryModels.ViewModels
{
    public class NewCarsInventoryViewModel : CarsInventoryViewModel, IMapTo<CarsInventoryViewModel>, IHaveCustomMappings
    {
        [JsonProperty("SortStrategyType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BaseCarSortStrategyType BaseSortStrategyType { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<NewCarsInventoryViewModel, CarsInventoryViewModel>()
                .ForMember(dest => dest.SortStrategyType, opt => opt.MapFrom(src => src.BaseSortStrategyType));
        }
    }
}

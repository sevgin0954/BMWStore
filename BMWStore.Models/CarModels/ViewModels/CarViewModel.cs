using AutoMapper;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarViewModel : BaseCarScheduleTestDriveViewModel, IMapFrom<UsedCar>, IMapFrom<NewCar>, IHaveCustomMappings
    {
        public double Acceleration_0_100Km { get; set; }

        public int CO2Emissions { get; set; }

        public string ColorName { get; set; }

        public double Displacement { get; set; }

        public int DoorsCount { get; set; }

        public string EngineName { get; set; }

        public double FuelConsumation_City_Litres_100Km { get; set; }

        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        public string FuelTypeName { get; set; }

        public double HoursePower { get; set; }

        public IEnumerable<string> OptionNames { get; set; } = new List<string>();

        public IEnumerable<string> PicturePublicIds { get; set; } = new List<string>();

        public decimal Torque { get; set; }

        public int Weight_Kg { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarViewModel>()
                .ForMember(dest => dest.OptionNames, opt => opt.MapFrom(src => src.Options.Select(o => o.Option.Name)))
                .ForMember(dest => dest.PicturePublicIds, opt => opt.MapFrom(src => src.Pictures.Select(p => p.PublicId)));

            base.CreateMappings(configuration);
        }
    }
}

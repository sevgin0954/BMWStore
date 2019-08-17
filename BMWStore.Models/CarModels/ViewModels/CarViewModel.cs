using AutoMapper;
using BMWStore.Entities;
using BMWStore.Models.OptionModels.ViewModels;
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

        public string Description { get; set; }

        public int DoorsCount { get; set; }

        public string EngineName { get; set; }

        public string TransmissionName { get; set; }

        public double FuelConsumation_City_Litres_100Km { get; set; }

        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        public string FuelTypeName { get; set; }

        public double HoursePower { get; set; }

        public string ModelTypeId { get; set; }

        public IEnumerable<OptionConciseViewModel> Options { get; set; } = new List<OptionConciseViewModel>();

        public IEnumerable<string> PicturePublicIds { get; set; } = new List<string>();

        public decimal Torque { get; set; }

        public int Weight_Kg { get; set; }

        public override void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<BaseCar, CarViewModel>()
                .ForMember(dest => dest.PicturePublicIds, opt => opt.MapFrom(src => src.Pictures.Select(p => p.PublicId)))
                .ForMember(dest => dest.TransmissionName, opt => opt.MapFrom(src => src.Engine.Transmission.Name))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options.Select(co => co.Option)));

            base.CreateMappings(configuration);
        }
    }
}

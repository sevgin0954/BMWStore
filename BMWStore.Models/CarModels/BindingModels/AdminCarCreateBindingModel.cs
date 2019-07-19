using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Common.Enums;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BMWStore.Models.CarModels.BindingModels
{
    public class AdminCarCreateBindingModel : IMapTo<NewCar>, IHaveCustomMappings
    {
        public CarCondition CarCondition { get; set; }

        public double Mileage { get; set; }

        [Display(Name ="Acceleration 0-100Km")]
        [Range(EntitiesConstants.CarMinAcceleration_0_100Km, EntitiesConstants.CarMaxAcceleration_0_100Km)]
        [Required]
        public double Acceleration_0_100Km { get; set; }

        [Range(EntitiesConstants.CarMinCO2Emissions, EntitiesConstants.CarMaxCO2Emissions)]
        [Required]
        public int CO2Emissions { get; set; }

        [Required]
        public string ColorName { get; set; }

        [Range(EntitiesConstants.CarMinDisplacement, EntitiesConstants.CarMaxDisplacement)]
        [Required]
        public double Displacement { get; set; }

        [Range(EntitiesConstants.CarMinDoorsCount, EntitiesConstants.CarMaxDoorsCount)]
        [Required]
        public int DoorsCount { get; set; }

        [Required]
        public string SelectedEngineId { get; set; }
        public IEnumerable<SelectListItem> Engines { get; set; } = new List<SelectListItem>();

        [Display(Name = "FuelConsumation City in Litres 100Km")]
        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_City_Litres_100Km)]
        [Required]
        public double FuelConsumation_City_Litres_100Km { get; set; }

        [Display(Name = "FuelConsumation Highway in Litres 100Km")]
        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_Highway_Litres_100Km)]
        [Required]
        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        [Required]
        public string SelectedFuelTypeId { get; set; }
        public IEnumerable<SelectListItem> FuelTypes { get; set; }

        [Range(EntitiesConstants.CarMinHoursePower, EntitiesConstants.CarMaxHoursePower)]
        [Required]
        public double HoursePower { get; set; }

        [Required]
        public string SelectedModelTypeId { get; set; }
        public IEnumerable<SelectListItem> ModelTypes { get; set; } = new List<SelectListItem>();

        [MaxLength(EntitiesConstants.CarNameMaxLength)]
        [MinLength(EntitiesConstants.CarNameMinLength)]
        [Required]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> CarOptions { get; set; } = new List<SelectListItem>();

        [Required]
        public IEnumerable<IFormFile> Pictures { get; set; }

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.CarMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string SelectedSeriesId { get; set; }
        public IEnumerable<SelectListItem> Series { get; set; } = new List<SelectListItem>();

        [Range(EntitiesConstants.CarMinTorque, EntitiesConstants.CarMaxTorque)]
        [Required]
        public decimal Torque { get; set; } 

        [MaxLength(EntitiesConstants.CarVinLength)]
        [MinLength(EntitiesConstants.CarVinLength)]
        [Required]
        public string Vin { get; set; }

        [Range(EntitiesConstants.CarMinWarrantyMonthsLeft, EntitiesConstants.CarMaxWarrantyMonthsLeft)]
        [Required]
        public int WarrantyMonthsLeft { get; set; }

        [Display(Name = "Weight in kg")]
        [Range(EntitiesConstants.CarMinWeight, EntitiesConstants.CarMaxWeight)]
        [Required]
        public int Weight_Kg { get; set; }

        [MinLength(4)]
        public string Year { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AdminCarCreateBindingModel, NewCar>()
                .ForMember(dest => dest.EngineId, opt => opt.MapFrom(src => src.SelectedEngineId))
                .ForMember(dest => dest.FuelTypeId, opt => opt.MapFrom(src => src.SelectedFuelTypeId))
                .ForMember(dest => dest.ModelTypeId, opt => opt.MapFrom(src => src.SelectedModelTypeId))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.SelectedSeriesId))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.CarOptions.Where(co => co.Selected)))
                .ForMember(dest => dest.Series, opt => opt.Ignore())
                .ForMember(dest => dest.Pictures, opt => opt.Ignore());
        }
    }
}

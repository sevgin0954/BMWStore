﻿using AutoMapper;
using BMWStore.Common.Constants;
using BMWStore.Entities;
using MappingRegistrar.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BMWStore.Models.CarModels.BindingModels
{
    public class AdminCarEditBindingModel : IMapTo<AdminCarEditBindingModel>, IMapFrom<NewCar>, IMapFrom<UsedCar>, IMapFrom<BaseCar>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Range(EntitiesConstants.CarMinMileage, EntitiesConstants.CarMaxMileage)]
        [Required]
        public double Mileage { get; set; }

        public bool IsNew { get; set; }

        [Display(Name = "Acceleration 0-100Km")]
        [Range(EntitiesConstants.CarMinAcceleration_0_100Km, EntitiesConstants.CarMaxAcceleration_0_100Km)]
        [Required]
        public double Acceleration_0_100Km { get; set; }

        public IEnumerable<SelectListItem> CarOptions { get; set; } = new List<SelectListItem>();

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

        [Required]
        public IEnumerable<IFormFile> Pictures { get; set; } = new List<IFormFile>();

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
            // TODO: REFACTOR MAPPING
            configuration.CreateMap<AdminCarEditBindingModel, AdminCarEditBindingModel>()
                .ForMember(dest => dest.Engines, opt => opt.Ignore())
                .ForMember(dest => dest.ModelTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Series, opt => opt.Ignore())
                .ForMember(dest => dest.FuelTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Pictures, opt => opt.Ignore())
                .ForMember(dest => dest.CarOptions, opt => opt.Ignore());

            configuration.CreateMap<AdminCarEditBindingModel, BaseCar>()
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.CarOptions.Where(o => o.Selected)))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Series, opt => opt.Ignore())
                .ForMember(dest => dest.EngineId, opt => opt.MapFrom(src => src.SelectedEngineId))
                .ForMember(dest => dest.FuelTypeId, opt => opt.MapFrom(src => src.SelectedFuelTypeId))
                .ForMember(dest => dest.ModelTypeId, opt => opt.MapFrom(src => src.SelectedModelTypeId))
                .ForMember(dest => dest.SeriesId, opt => opt.MapFrom(src => src.SelectedSeriesId))
                .ForMember(dest => dest.Pictures, opt => opt.Ignore())
                .IncludeAllDerived();

            configuration.CreateMap<BaseCar, AdminCarEditBindingModel>()
                .ForMember(dest => dest.SelectedEngineId, opt => opt.MapFrom(src => src.EngineId))
                .ForMember(dest => dest.SelectedFuelTypeId, opt => opt.MapFrom(src => src.FuelTypeId))
                .ForMember(dest => dest.SelectedModelTypeId, opt => opt.MapFrom(src => src.ModelTypeId))
                .ForMember(dest => dest.SelectedSeriesId, opt => opt.MapFrom(src => src.SeriesId))
                .ForMember(dest => dest.CarOptions, opt => opt.MapFrom(src => src.Options.Select(o => new SelectListItem()
                { Value = o.OptionId, Text = o.Option.Name, Selected = true })))
                .ForMember(dest => dest.Pictures, opt => opt.Ignore())
                .ForMember(dest => dest.Engines, opt => opt.Ignore())
                .ForMember(dest => dest.FuelTypes, opt => opt.Ignore())
                .ForMember(dest => dest.ModelTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Series, opt => opt.Ignore())
                .IncludeAllDerived();
        }
    }
}
﻿using BMWStore.Common.Constants;
using BMWStore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Models.CarModels.BindingModels
{
    public class AdminCarEditBindingModel
    {
        public string Id { get; set; }

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

        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_City_Litres_100Km)]
        [Required]
        public double FuelConsumation_City_Litres_100Km { get; set; }

        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_Highway_Litres_100Km)]
        [Required]
        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        [Required]
        public string SelectedFuelTypeId { get; set; }
        public IEnumerable<SelectListItem> FuelTypes { get; set; }

        [Range(EntitiesConstants.CarMinHoursePower, EntitiesConstants.CarMaxHoursePower)]
        [Required]
        public double HoursePower { get; set; }

        [Range(EntitiesConstants.CarMinMileage, EntitiesConstants.CarMaxMileage)]
        [Required]
        public double Mileage { get; set; }

        [Required]
        public string SelectedModelTypeId { get; set; }
        public IEnumerable<SelectListItem> ModelTypes { get; set; } = new List<SelectListItem>();

        [MaxLength(EntitiesConstants.CarNameMaxLength)]
        [MinLength(EntitiesConstants.CarNameMinLength)]
        [Required]
        public string Name { get; set; }

        public ICollection<CarOption> Options { get; set; } = new List<CarOption>();

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.CarMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string SeriesId { get; set; }
        public Series Series { get; set; }

        [Range(EntitiesConstants.CarMinTorque, EntitiesConstants.CarMaxTorque)]
        [Required]
        public decimal Torque { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        [MaxLength(EntitiesConstants.CarVinLength)]
        [MinLength(EntitiesConstants.CarVinLength)]
        [Required]
        public string Vin { get; set; }

        [Range(EntitiesConstants.CarMinWarrantyMonthsLeft, EntitiesConstants.CarMaxWarrantyMonthsLeft)]
        [Required]
        public int WarrantyMonthsLeft { get; set; }

        [Range(EntitiesConstants.CarMinWeight, EntitiesConstants.CarMaxWeight)]
        [Required]
        public int Weight_Kg { get; set; }

        [MinLength(4)]
        public string Year { get; set; }
    }
}

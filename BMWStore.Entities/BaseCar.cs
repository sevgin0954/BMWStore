using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public abstract class BaseCar
    {
        public string Id { get; set; }

        [Range(EntitiesConstants.CarMinAcceleration_0_100Km, EntitiesConstants.CarMaxAcceleration_0_100Km)]
        [Required]
        public double Acceleration_0_100Km { get; set; }

        [Range(EntitiesConstants.CarMinCO2Emissions, EntitiesConstants.CarMaxCO2Emissions)]
        [Required]
        public int CO2Emissions { get; set; }


        [Range(EntitiesConstants.CarMinDisplacement, EntitiesConstants.CarMaxDisplacement)]
        [Required]
        public double Displacement { get; set; }

        [Range(EntitiesConstants.CarMinDoorsCount, EntitiesConstants.CarMaxDoorsCount)]
        [Required]
        public int DoorsCount { get; set; }

        [Required]
        public string EngineId { get; set; }
        public Engine Engine { get; set; }

        [Required]
        public string ExteriorId { get; set; }
        public Exterior Exterior { get; set; }

        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_City_Litres_100Km)]
        [Required]
        public double FuelConsumation_City_Litres_100Km { get; set; }

        [Range(EntitiesConstants.CarMinFuelConsumation, EntitiesConstants.CarMaxFuelConsumation_Highway_Litres_100Km)]
        [Required]
        public double FuelConsumation_Highway_Litres_100Km { get; set; }

        [Required]
        public string FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        [Range(EntitiesConstants.CarMinHoursePower, EntitiesConstants.CarMaxHoursePower)]
        [Required]
        public double HoursePower { get; set; }

        [Required]
        public string ModelTypeId { get; set; }
        public ModelType ModelType { get; set; }

        [Range(EntitiesConstants.CarNameMinLength, EntitiesConstants.CarNameMaxLength)]
        [Required]
        public string Name { get; set; }

        public ICollection<Option> Options { get; set; } = new List<Option>();

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.CarMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string SeriesId { get; set; }
        public Series Series { get; set; }

        [Range(EntitiesConstants.CarMaxTorque, EntitiesConstants.CarMinTorque)]
        [Required]
        public decimal Torque { get; set; }

        public ICollection<UserBoughtCar> Buyers { get; set; } = new List<UserBoughtCar>();

        [Range(EntitiesConstants.CarVinLength, EntitiesConstants.CarVinLength)]
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

using BMWStore.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class UsedCar : BaseCar
    {
        [Range(EntitiesConstants.CarMinMileage, EntitiesConstants.CarMaxMileage)]
        [Required]
        public double Mileage { get; set; }
    }
}
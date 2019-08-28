using System.Collections.Generic;

namespace BMWStore.Services.Models
{
    public class CarsFilterServiceModel
    {
        public List<FilterTypeServiceModel> Years { get; set; } = new List<FilterTypeServiceModel>();

        public List<FilterTypeServiceModel> Series { get; set; } = new List<FilterTypeServiceModel>();

        public List<FilterTypeServiceModel> ModelTypes { get; set; } = new List<FilterTypeServiceModel>();

        public List<FilterTypeServiceModel> Prices { get; set; } = new List<FilterTypeServiceModel>();
    }
}

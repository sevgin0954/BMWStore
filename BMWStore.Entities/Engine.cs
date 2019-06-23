using BMWStore.Common.Constants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BMWStore.Entities
{
    public class Engine
    {
        public string Id { get; set; }

        public ICollection<BaseCar> Cars { get; set; } = new List<BaseCar>();

        [Range(typeof(decimal), EntitiesConstants.MinPrice, EntitiesConstants.EngineMaxPrice)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string TransmissionId { get; set; }
        public Transmission Transmission { get; set; }

        [Range(typeof(decimal), EntitiesConstants.EngineMinWeight_Kg, EntitiesConstants.EngineMaxWeight_Kg)]
        [Required]
        public string Weight_Kg { get; set; }
    }
}
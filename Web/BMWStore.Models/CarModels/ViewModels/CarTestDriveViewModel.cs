using BMWStore.Services.Models;
using MappingRegistrar.Interfaces;

namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarTestDriveViewModel : IMapFrom<BaseCarServiceModel>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string PicturePublicId { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }

        public string Vin { get; set; }
    }
}

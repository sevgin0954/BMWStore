namespace BMWStore.Models.CarModels.ViewModels
{
    public class CarConciseViewModel
    {
        public string Id { get; set; }

        public double Mileage { get; set; }

        public string ModelTypeName { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SeriesName { get; set; }

        public string Vin { get; set; }

        public int WarrantyMonthsLeft { get; set; }

        public string Year { get; set; }
    }
}

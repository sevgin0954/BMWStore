using System.Linq;
using BMWStore.Data.FilterStrategies.CarStrategies.Interfaces;
using BMWStore.Entities;

namespace BMWStore.Data.FilterStrategies.CarStrategies
{
    public class FilterCarsByKeyWordStrategy : ICarFilterStrategy
    {
        private readonly string keyWord;

        public FilterCarsByKeyWordStrategy(string keyWord)
        {
            this.keyWord = keyWord.ToLower();
        }

        public IQueryable<BaseCar> Filter(IQueryable<BaseCar> cars)
        {
            var filteredCars = cars.Where(c =>
                c.Name.ToLower().Contains(this.keyWord)
                ||
                c.Series.Name.ToLower().Contains(this.keyWord)
                ||
                c.GetType().Name.ToLower().Contains(this.keyWord)
                ||
                c.ModelType.Name.ToLower().Contains(this.keyWord)
                ||
                c.Year == this.keyWord
                ||
                c.ColorName.ToLower() == this.keyWord
            );

            return filteredCars;
        }
    }
}

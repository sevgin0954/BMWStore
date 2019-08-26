using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class FuelTypeServiceModelMethods
    {
        public static FuelTypeServiceModel Create(string id)
        {
            var model = new FuelTypeServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static FuelTypeServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

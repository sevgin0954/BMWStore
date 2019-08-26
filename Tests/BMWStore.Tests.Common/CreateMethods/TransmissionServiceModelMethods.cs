using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class TransmissionServiceModelMethods
    {
        public static TransmissionServiceModel Create(string id)
        {
            var model = new TransmissionServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static TransmissionServiceModel Create(string id, decimal price)
        {
            var model = Create(id);
            model.Price = price;

            return model;
        }

        public static TransmissionServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

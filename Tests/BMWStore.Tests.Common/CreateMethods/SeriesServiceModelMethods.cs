using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class SeriesServiceModelMethods
    {
        public static SeriesServiceModel Create(string id)
        {
            var model = new SeriesServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static SeriesServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

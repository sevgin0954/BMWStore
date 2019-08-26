using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class ModelTypeServiceModelMethods
    {
        public static ModelTypeServiceModel Create(string id)
        {
            var model = new ModelTypeServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static ModelTypeServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

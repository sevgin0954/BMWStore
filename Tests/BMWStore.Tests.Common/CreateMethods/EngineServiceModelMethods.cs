using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class EngineServiceModelMethods
    {
        public static EngineServiceModel Create(string id)
        {
            var model = new EngineServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static EngineServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

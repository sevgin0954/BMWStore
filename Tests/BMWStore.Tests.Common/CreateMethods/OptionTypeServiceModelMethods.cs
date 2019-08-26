using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class OptionTypeServiceModelMethods
    {
        public static OptionTypeServiceModel Create(string id)
        {
            var model = new OptionTypeServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static OptionTypeServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

using BMWStore.Services.Models;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class OptionServiceModelMethods
    {
        public static OptionServiceModel Create(string id)
        {
            var model = new OptionServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static OptionServiceModel Create(string id, string name)
        {
            var model = Create(id);
            model.Name = name;

            return model;
        }
    }
}

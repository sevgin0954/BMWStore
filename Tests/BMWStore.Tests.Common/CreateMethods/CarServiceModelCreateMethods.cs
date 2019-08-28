using BMWStore.Entities;
using BMWStore.Services.Models;
using System.Linq;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class CarServiceModelCreateMethods
    {
        public static CarServiceModel Create(string id)
        {
            var model = new CarServiceModel()
            {
                Id = id
            };

            return model;
        }

        public static CarServiceModel Create(string id, params Option[] options)
        {
            var model = Create(id);
            model.Options = options.Select(o => new CarOptionServiceModel()
            {
                OptionId = o.Id,
                Option = new OptionServiceModel()
                {
                    Name = o.Name
                }
            }).ToList();

            return model;
        }

        public static CarServiceModel Create(string id, double mileage)
        {
            var model = Create(id);
            model.Mileage = mileage;

            return model;
        }

        public static CarServiceModel Create(string id, string picturrePublicId)
        {
            var model = Create(id);
            model.Pictures.Add(new PictureServiceModel() { PublicId = picturrePublicId });

            return model;
        }
    }
}

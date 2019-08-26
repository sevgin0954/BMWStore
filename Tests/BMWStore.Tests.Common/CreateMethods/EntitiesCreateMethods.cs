using BMWStore.Entities;

namespace BMWStore.Tests.Common.CreateMethods
{
    public static class EntitiesCreateMethods
    {
        public static CarOption CreateCarOption(Option option)
        {
            var carOption = new CarOption()
            {
                Option = option
            };

            return carOption;
        }

        public static Picture CreatePicture(string publicId)
        {
            var dbPicture = new Picture()
            {
                PublicId = publicId
            };

            return dbPicture;
        }
    }
}

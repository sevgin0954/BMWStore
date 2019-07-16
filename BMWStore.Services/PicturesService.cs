using BMWStore.Entities;
using BMWStore.Services.Interfaces;
using System.Collections.Generic;

namespace BMWStore.Services
{
    public class PicturesService : IPicturesService
    {
        public ICollection<Picture> GetPicturesFromByteData(IEnumerable<byte[]> data)
        {
            var pictures = new List<Picture>();

            foreach (var pictureData in data)
            {
                var picture = new Picture()
                {
                    Data = pictureData
                };
                pictures.Add(picture);
            }

            return pictures;
        }
    }
}

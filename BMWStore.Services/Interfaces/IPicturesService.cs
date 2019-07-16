using BMWStore.Entities;
using System.Collections.Generic;

namespace BMWStore.Services.Interfaces
{
    public interface IPicturesService
    {
        ICollection<Picture> GetPicturesFromByteData(IEnumerable<byte[]> data);
    }
}

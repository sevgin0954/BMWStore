using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BMWStore.Helpers
{
    public static class FileHelper
    {
        public static async Task<ICollection<byte[]>> IFormFilesToByteAsync(IEnumerable<IFormFile> files)
        {
            var pictures = new List<byte[]>();

            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    pictures.Add(memoryStream.ToArray());
                }
            }

            return pictures;
        }
    }
}
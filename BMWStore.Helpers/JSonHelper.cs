using Newtonsoft.Json;
using System.Text;

namespace BMWStore.Helpers
{
    public static class JSonHelper
    {
        public static byte[] Serialize(object obj)
        {
            var objAsString = JsonConvert.SerializeObject(obj);
            var objAsByteArray = Encoding.Default.GetBytes(objAsString);

            return objAsByteArray;
        }

        public static TOject Desirialize<TOject>(byte[] data)
        {
            var dataAsString = Encoding.Default.GetString(data);
            var desirializedObject = JsonConvert.DeserializeObject<TOject>(dataAsString);

            return desirializedObject;
        }
    }
}
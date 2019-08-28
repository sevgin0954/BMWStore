using System.Reflection;

namespace BMWStore.Tests.Common.GetMethods
{
    public static class GetPrivateMethods
    {
        public static object GetField(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(
                         fieldName,
                         BindingFlags.NonPublic |
                         BindingFlags.Instance);
            var value = field.GetValue(obj);

            return value;
        }
    }
}

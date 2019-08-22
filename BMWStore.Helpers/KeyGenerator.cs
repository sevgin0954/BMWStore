using System.Collections.Generic;
using System.Text;

namespace BMWStore.Helpers
{
    public static class KeyGenerator
    {
        public static string Generate(string prepend, object obj, IEnumerable<string> strs, params string[] values)
        {
            var resultKey = new StringBuilder();

            resultKey.Append(prepend);
            AppendAssemlyName(obj, resultKey);
            AppendValues(strs, resultKey);
            AppendValues(values, resultKey);

            return resultKey.ToString();
        }

        public static string Generate(string prepend, object obj, params string[] values)
        {
            var resultKey = new StringBuilder();

            resultKey.Append(prepend);
            AppendAssemlyName(obj, resultKey);
            AppendValues(values, resultKey);

            return resultKey.ToString();
        }

        private static void AppendAssemlyName(object obj, StringBuilder stringBuilder)
        {
            var objName = obj.GetType().AssemblyQualifiedName;
            stringBuilder.Append(objName);
        }

        private static void AppendValues(IEnumerable<string> values, StringBuilder stringBuilder)
        {
            foreach (var value in values)
            {
                if (value == null)
                {
                    stringBuilder.Append("null");
                }
                else
                {
                    stringBuilder.Append(value);
                }
            }
        }
    }
}

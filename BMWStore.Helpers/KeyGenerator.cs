using System.Collections.Generic;
using System.Text;

namespace BMWStore.Helpers
{
    public static class KeyGenerator
    {
        public static string Generate(string prepend, IEnumerable<string> strs, params string[] values)
        {
            var resultKey = new StringBuilder();

            resultKey.Append(prepend);
            AppendValues(strs, resultKey);
            AppendValues(values, resultKey);

            return resultKey.ToString();
        }

        public static string Generate(string prepend, params string[] values)
        {
            var resultKey = new StringBuilder();

            resultKey.Append(prepend);
            AppendValues(values, resultKey);

            return resultKey.ToString();
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

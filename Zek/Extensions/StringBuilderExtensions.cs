using System.Text;

namespace Zek.Extensions
{
    /// <summary>
    /// Extensions for StringBuilder
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// AppendLine version with format string parameters.
        /// </summary>
        public static void AppendLine(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.AppendLine(string.Format(value, parameters));
        }

        public static void RemoveStart(this StringBuilder sb, string text)
        {
            if (sb.ToString().StartsWith(text))
                sb.Remove(0, text.Length);
        }
        public static void RemoveStart(this StringBuilder sb, int length)
        {
            sb.Remove(0, length);
        }
        public static void RemoveEnd(this StringBuilder sb, int length)
        {
            sb.Remove(sb.Length - length, length);
        }
        public static void RemoveEnd(this StringBuilder sb, string text)
        {
            if (sb.ToString().EndsWith(text))
                sb.Remove(sb.Length - text.Length, text.Length);
        }
    }
}
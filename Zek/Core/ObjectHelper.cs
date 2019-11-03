using Zek.Extensions;

namespace Zek.Core
{
    public class ObjectHelper
    {
        /// <summary>
        /// Returns first not null and empty value from array
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object CoalesceEmpty(params object[] args)
        {
            if (args == null) return null;
            foreach (var val in args)
            {
                if (!val.IsNullOrDefault())
                    return val;
            }

            return null;
        }
        /// <summary>
        /// Returns first not null value from array
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object Coalesce(params object[] args)
        {
            if (args == null) return null;

            foreach (var val in args)
            {
                if (val != null)
                    return val;
            }

            return null;
        }
    }
}

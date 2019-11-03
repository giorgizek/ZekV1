using System;

namespace Zek.Extensions
{
    /// <summary>
    /// Class contains extension methods to work with Boolean types
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Returns a binary (or "bit") representation of the Boolean value. True returns 1; False returns 0
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int ToBinary(this bool b)
        {
            return b ? 1 : 0;
        }

        /// <summary>
        /// Executes the Action if the Boolean value is True
        /// </summary>
        /// <param name="b"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IfTrue(this bool b, Action action)
        {
            if (b)
            {
                action();
            }
            return b;
        }

        /// <summary>
        /// Executes the Action if the Boolean value is False
        /// </summary>
        /// <param name="b"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool IfFalse(this bool b, Action action)
        {
            if (!b)
            {
                action();
            }
            return b;
        }
    }
}
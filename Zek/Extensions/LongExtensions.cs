﻿namespace Zek.Extensions
{
    public static class LongExtensions
    {
        #region "Between"

        /// <summary>
        /// Returns a Boolean value indicating that the integer is "between" the low and high values specified. Ex: 1 is between 0 and 2, but -1 or 3 are not.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static bool Between(this long i, long low, long high)
        {
            return i >= low && i <= high;
        }
        /// <summary>
        /// Returns a Boolean value indicating that the integer is "not between" the low and high values specified. Ex: -1 or 3 are not between 0 and 2, but 1 is.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static bool NotBetween(this long i, long low, long high)
        {
            return !Between(i, low, high);
        }

        #endregion
    }
}

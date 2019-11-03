﻿namespace Zek.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks to see if the object has null default value for basic types
        /// </summary>
        /// <typeparam name="T">Type of object being passed</typeparam>
        /// <param name="value">Object whose value needs to be checked</param>
        /// <returns>true if the value is null default. Otherwise returns false</returns>
        public static bool IsDefault<T>(this T value)
        {
            return Equals(value, default(T));
        }

/*
        /// <summary>
        /// ადარებს ორ მნიშვნელობას ერთმანეთს.
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns>გვიბრუნებს true-ს, როცა გადმოცემული პარამეტრები ერთმანეთის ტოლია.</returns>
        public static bool Compare(this object val1, object val2)
        {
            if (val1 != null && val2 != null && val1.Equals(val2)) return true;
            if (val1 == val2) return true;
            return false;
        }*/
    }
}

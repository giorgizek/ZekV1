using System;
using System.Collections.Generic;
using System.Linq;

namespace Zek.Extensions
{
    public static class EnumExtensions
    {
        public static bool HasFlag(this Enum value, Enum flag)
        {
            var val = Convert.ToInt32(flag);
            return (Convert.ToInt32(value) & val) == val;
        }
        
        public static int ToInt32(this Enum value)
        {
            return Convert.ToInt32(value);
        }
        public static byte ToByte(this Enum value)
        {
            return Convert.ToByte(value);
        }

        public static T AddFlag<T>(this Enum flags, T flagToAdd)
        {
            return AddFlags(flags, flagToAdd);
        }
        public static T AddFlags<T>(this Enum flags, params T[] flagsToAdd)
        {
            try
            {
                if (flagsToAdd == null) return (T)(object)flags;

                var result = (int)(object)flags;
                for (var i = 0; i < flagsToAdd.Length; i++)
                {
                    result |= (int)(object)flagsToAdd[i];
                }

                return (T)(object)result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof (T).Name}'.", ex);
            }
        }

        public static T DeleteFlag<T>(this Enum flags, T flagToDelete)
        {
            return DeleteFlags(flags, flagToDelete);
        }
        public static T DeleteFlags<T>(this Enum flags, params T[] flagsToDelete)
        {
            try
            {
                if (flagsToDelete == null) return (T)(object)flags;

                var result = (int)(object)flags;
                for (var i = 0; i < flagsToDelete.Length; i++)
                {
                    result &= ~(int)(object)flagsToDelete[i];
                }
                return (T)(object)result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof (T).Name}'.", ex);
            }
        }

        public static T ToggleFlag<T>(this Enum flags, T flagToToggle)
        {
            var result = (int)(object)flags ^ (int)(object)flagToToggle;
            return (T)(object)result;
        }



        public static T ToEnum<T>(this string str) where T : struct
        {
            return (T)Enum.Parse(typeof(T), str);
        }

        public static T ToEnum<T>(this string str, bool ignoreCase) where T : struct
        {
            return (T)Enum.Parse(typeof(T), str, ignoreCase);
        }

        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

        public static bool IsDefined<T>(T value) where T : struct
        {
            return Enum.IsDefined(typeof(T), value);
        }



        public static bool TryParse<T>(this Enum @enum, string value, out T result)
        {
            value = value.Replace(' ', '_');
            if (Enum.IsDefined(typeof(T), value))
            {
                result = (T)Enum.Parse(typeof(T), value, true);
                return true;
            }

            foreach (var item in Enum.GetNames(typeof(T)))
            {
                result = (T)Enum.Parse(typeof(T), item);
                return true;
            }
            result = default(T);
            return false;
        }

        public static IEnumerable<Enum> UntypedGetValues(Type type)
        {
            return Enum.GetValues(type).Cast<Enum>();
        }
    }
}
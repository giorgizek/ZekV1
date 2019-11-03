using System;
using System.Reflection;
using Zek.Utils;

namespace Zek.Extensions
{
    public static class EnumExtensions
    {
        public static bool HasFlag(this Enum value, Enum flag)
        {
            var val = Convert.ToInt32(flag);
            return (Convert.ToInt32(value) & val) == val;
        }

        public static T AddFlags<T>(this Enum flags, params T[] flagsToAdd)
        {
            try
            {
                if (flagsToAdd == null)
                    return (T)(object)flags;

                var result = (int)(object)flags;
                for (var i = 0; i < flagsToAdd.Length; i++)
                {
                    result |= (int)(object)flagsToAdd[i];
                }

                return (T)(object)result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof(T).Name}'.", ex);
            }
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
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof(T).Name}'.", ex);
            }
        }

        public static T ToggleFlag<T>(this Enum flags, T flagToToggle)
        {
            var result = (int)(object)flags ^ (int)(object)flagToToggle;
            return (T)(object)result;
        }


        public static int ToInt32(this Enum value)
        {
            return Convert.ToInt32(value);
        }


        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
                return null;

            return DisplayNameHelper.GetDisplayNameForProperty(field);

            //var attributes = field.GetCustomAttributes(typeof(DisplayAttribute), true).ToArray();
            //return attributes.Length > 0 ? ((DisplayAttribute)attributes[0]).GetName() : value.ToString();
        }
    }
}
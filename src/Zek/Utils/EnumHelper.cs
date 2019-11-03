using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Zek.Utils
{
    public static class EnumHelper
    {

        public static Dictionary<TEnum, string> GetEnumDisplayNameDictionary<TEnum>()
        {
            var values = Enum.GetValues(typeof(TEnum));
            var result = new Dictionary<TEnum, string>();
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in values)
            {
                var field = value.GetType().GetField(value.ToString());
                if (field == null) continue;
                var attribute = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();

                result.Add((TEnum)value, attribute != null ? attribute.GetName() : value.ToString());
            }

            return result;
        }
    }
}

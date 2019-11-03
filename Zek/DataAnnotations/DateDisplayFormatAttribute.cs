using System;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateDisplayFormatAttribute : DisplayFormatAttribute
    {
        public DateDisplayFormatAttribute()
        {
            DataFormatString = "{0:yyyy-MM-dd}";
            ApplyFormatInEditMode = true;
        }
    }
}

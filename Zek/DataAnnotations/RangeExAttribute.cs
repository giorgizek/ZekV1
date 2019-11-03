using System;
using System.ComponentModel.DataAnnotations;
using Zek.Properties;

namespace Zek.DataAnnotations
{
    public class RangeExAttribute : RangeAttribute
    {
        public RangeExAttribute(Type type, string minimum, string maximum)
            : base(type, minimum, maximum)
        {
            ErrorMessageResourceName = "RangeAttribute_ValidationError";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }
        public RangeExAttribute(double minimum, double maximum)
            : base(minimum, maximum)
        {
            ErrorMessageResourceName = "RangeAttribute_ValidationError";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }
        public RangeExAttribute(int minimum, int maximum) : base(minimum, maximum)
        {
            ErrorMessageResourceName = "RangeAttribute_ValidationError";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }
    }
}

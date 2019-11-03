using System;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    public class YearRangeAttribute : RangeAttribute
    {
        public YearRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
        {
        }

        public YearRangeAttribute(int minimum) : base(minimum, DateTime.Today.Year)
        {

        }
    }
}

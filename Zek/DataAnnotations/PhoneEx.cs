using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Zek.Properties;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneExAttribute : DataTypeAttribute
    {
        private static readonly Regex Regex = new Regex(@"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public PhoneExAttribute()
            : base(DataType.PhoneNumber)
        {
            ErrorMessageResourceName = "PhoneAttribute_Invalid";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }
        
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var input = value as string;
            return (input != null) && (Regex.Match(input).Length > 0);
        }
    }
}

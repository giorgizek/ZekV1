using System.ComponentModel.DataAnnotations;
using Zek.Properties;

namespace Zek.DataAnnotations
{
    public class StringLengthExAttribute : StringLengthAttribute
    {
        public StringLengthExAttribute(int maximumLength)
            : base(maximumLength)
        {
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }

        public StringLengthExAttribute(int minimumLength, int maximumLength)
            : this(maximumLength)
        {
            MinimumLength = minimumLength;
        }
    }
}

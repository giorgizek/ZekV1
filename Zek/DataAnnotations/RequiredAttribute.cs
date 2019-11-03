using System.ComponentModel.DataAnnotations;
using Zek.Properties;

namespace Zek.DataAnnotations
{
    public class RequiredExAttribute : RequiredAttribute
    {
        public RequiredExAttribute()
        {
            AllowEmptyStrings = false;
            ErrorMessageResourceName = "RequiredAttribute_ValidationError";
            ErrorMessageResourceType = typeof(DataAnnotationsResources);
        }
    }
}

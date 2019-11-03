using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.Dictionary
{
    /// <summary>
    /// Gender
    /// </summary>
    public enum Gender : byte
    {
        /// <summary>
        /// Male
        /// </summary>
        [Display(Name = nameof(PersonResources.Male), ResourceType = typeof(PersonResources))]
        Male = 1,

        /// <summary>
        /// Female
        /// </summary>
        [Display(Name = nameof(PersonResources.Female), ResourceType = typeof(PersonResources))]
        Female = 2,
    }
}

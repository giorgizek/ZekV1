using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Contact
{
    public class AddWebsiteViewModel
    {
        [StringLength(1024, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Url]
        [Display(Name = nameof(ContactResources.Website), ResourceType = typeof(ContactResources))]
        public string Website { get; set; }
    }
}
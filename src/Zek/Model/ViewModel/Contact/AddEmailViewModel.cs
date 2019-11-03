using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Contact
{
    public class AddEmailViewModel
    {
        [StringLength(256, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email), ResourceType = typeof(EmailResources))]
        public string Email { get; set; }
    }
}
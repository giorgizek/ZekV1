using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Contact
{
    public class AddPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone), ResourceType = typeof(ContactResources))]
        public string PhoneNumber { get; set; }
    }
}

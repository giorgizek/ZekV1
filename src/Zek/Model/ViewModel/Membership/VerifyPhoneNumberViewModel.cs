using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class VerifyPhoneNumberViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ApplicationResources.Code), ResourceType = typeof(ApplicationResources))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone), ResourceType =typeof(ContactResources))]
        public string PhoneNumber { get; set; }
    }
}

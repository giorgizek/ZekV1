using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class ChangePasswordViewModel
    {
        //public bool HasPassword { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        //[RequiredIf(nameof(HasPassword), true, ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(MembershipResources.CurrentPassword), ResourceType = typeof(MembershipResources))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(MembershipResources.NewPassword), ResourceType = typeof(MembershipResources))]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = nameof(MembershipResources.ConfirmPassword), ResourceType = typeof(MembershipResources))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(MembershipResources.ConfirmPasswordErrorText), ErrorMessageResourceType = typeof(MembershipResources))]
        public string ConfirmPassword { get; set; }
    }
}

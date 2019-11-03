using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class LoginBaseViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(MembershipResources.Password), ResourceType = typeof(MembershipResources))]
        public string Password { get; set; }

        [Display(Name = nameof(MembershipResources.RememberMe), ResourceType = typeof(MembershipResources))]
        public bool RememberMe { get; set; }
    }


    public class LoginViewModel : LoginBaseViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(MembershipResources.UserName), ResourceType = typeof(MembershipResources))]
        public string UserName { get; set; }
    }

    public class EmailLoginViewModel : LoginBaseViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email), ResourceType = typeof(EmailResources))]
        public string Email { get; set; }

    }
}

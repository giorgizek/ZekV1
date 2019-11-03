using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels.Account
{
    public class RegisterViewModel
    {
        [RequiredEx]
        [EmailAddressEx]
        [Display(Name = "Email", ResourceType = typeof(ApplicationResources))]
        public string Email { get; set; }

        [RequiredEx]
        [StringLengthEx(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(MembershipResources))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(MembershipResources))]
        [Compare("Password", ErrorMessageResourceName = "PasswordAndConfirmPasswordDoNotMatch", ErrorMessageResourceType = typeof(MembershipResources))]
        public string ConfirmPassword { get; set; }

        [RequiredEx]
        [Display(Name = "TermsOfService", ResourceType = typeof(MembershipResources))]
        public bool TermsOfService { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels.Account
{
    public class LoginViewModel
    {
        [RequiredEx]
        [Display(Name = "Email", ResourceType = typeof(ContactResources))]
        [EmailAddressEx]
        public string Email { get; set; }

        [RequiredEx]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(MembershipResources))]
        public string Password { get; set; }

        [Display(Name = "RemerberMe", ResourceType = typeof(MembershipResources))]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [RequiredEx]
        [EmailAddressEx]
        [Display(Name = "Email", ResourceType = typeof(ApplicationResources))]
        public string Email { get; set; }
    }
}

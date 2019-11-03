using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class ManageViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress]
        [Display(Name = nameof(EmailResources.Email), ResourceType = typeof(EmailResources))]
        public string Email { get; set; }

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone), ResourceType = typeof(ContactResources))]
        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }


        public MembershipPersonViewModel Person { get; set; }


        public ChangePasswordViewModel ChangePassword { get; set; }
    }
}

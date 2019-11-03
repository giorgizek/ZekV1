using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class VerifyCodeViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        public string Provider { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}

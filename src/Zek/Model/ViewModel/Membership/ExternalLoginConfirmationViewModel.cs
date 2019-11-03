using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Membership
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        public string Email { get; set; }
    }
}

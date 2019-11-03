using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Contact
{
    public class ContactBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName = nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone1), ResourceType = typeof(ContactResources))]
        public string Phone1 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName = nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Fax1), ResourceType = typeof(ContactResources))]
        public string Fax1 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName = nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Mobile1), ResourceType = typeof(ContactResources))]
        public string Mobile1 { get; set; }


        [StringLength(256, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email1), ResourceType = typeof(EmailResources))]
        public string Email1 { get; set; }


        [StringLength(2083, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Url(ErrorMessageResourceName = nameof(DataAnnotationsResources.UrlAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Url), ResourceType = typeof(ContactResources))]
        public string Url { get; set; }
    }
    public class ContactViewModel : ContactBaseViewModel
    {
        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone2), ResourceType = typeof(ContactResources))]
        public string Phone2 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Phone3), ResourceType = typeof(ContactResources))]
        public string Phone3 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Fax2), ResourceType = typeof(ContactResources))]
        public string Fax2 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Fax3), ResourceType = typeof(ContactResources))]
        public string Fax3 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Mobile2), ResourceType = typeof(ContactResources))]
        public string Mobile2 { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Phone(ErrorMessageResourceName =nameof(DataAnnotationsResources.PhoneAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Mobile3), ResourceType = typeof(ContactResources))]
        public string Mobile3 { get; set; }

        [StringLength(256, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email2), ResourceType = typeof(EmailResources))]
        public string Email2 { get; set; }

        [StringLength(256, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email3), ResourceType = typeof(EmailResources))]
        public string Email3 { get; set; }
    }
}
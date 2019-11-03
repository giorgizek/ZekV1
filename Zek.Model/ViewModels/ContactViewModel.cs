using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels
{
    public class ContactViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Phone1", ResourceType = typeof(ContactResources))]
        public string Phone1 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Phone2", ResourceType = typeof(ContactResources))]
        public string Phone2 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Phone3", ResourceType = typeof(ContactResources))]
        public string Phone3 { get; set; }

        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Fax1", ResourceType = typeof(ContactResources))]
        public string Fax1 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Fax2", ResourceType = typeof(ContactResources))]
        public string Fax2 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Fax3", ResourceType = typeof(ContactResources))]
        public string Fax3 { get; set; }

        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Mobile1", ResourceType = typeof(ContactResources))]
        public string Mobile1 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Mobile2", ResourceType = typeof(ContactResources))]
        public string Mobile2 { get; set; }
        [StringLengthEx(25)]
        [PhoneEx]
        [Display(Name = "Mobile3", ResourceType = typeof(ContactResources))]
        public string Mobile3 { get; set; }

        [StringLengthEx(256)]
        [EmailAddressEx]
        [Display(Name = "Email1", ResourceType = typeof(ContactResources))]
        public string Email1 { get; set; }
        [StringLengthEx(256)]
        [EmailAddressEx]
        [Display(Name = "Email2", ResourceType = typeof(ContactResources))]
        public string Email2 { get; set; }
        [StringLengthEx(256)]
        [EmailAddressEx]
        [Display(Name = "Email3", ResourceType = typeof(ContactResources))]
        public string Email3 { get; set; }

        
        [StringLengthEx(1024)]
        [UrlEx]
        [Display(Name = "Url", ResourceType = typeof(ContactResources))]
        public string Url { get; set; }


    }
}

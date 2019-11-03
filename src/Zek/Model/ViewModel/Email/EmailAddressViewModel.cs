using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Email
{
    public class EmailAddressViewModel
    {
        public EmailAddressViewModel()
        {
        }
        //public EmailAddressViewModel(string name, string address)
        //{
        //    Name = name;
        //    Address = address;
        //}
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Email), ResourceType = typeof(EmailResources))]
        public string Address { get; set; }

        [Display(Name = nameof(EmailResources.Name), ResourceType = typeof(EmailResources))]
        public string Name { get; set; }

    }
}

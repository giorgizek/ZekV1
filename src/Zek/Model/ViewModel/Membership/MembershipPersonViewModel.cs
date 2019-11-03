using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.Dictionary;

namespace Zek.Model.ViewModel.Membership
{
    public class MembershipPersonViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.FirstName), ResourceType = typeof(PersonResources))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.LastName), ResourceType = typeof(PersonResources))]
        public string LastName { get; set; }


        [Display(Name = nameof(PersonResources.Gender), ResourceType = typeof(PersonResources))]
        public Gender GenderId { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = nameof(PersonResources.BirthDate), ResourceType = typeof(PersonResources))]
        public DateTime? BirthDate { get; set; }
    }
}

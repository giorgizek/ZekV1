using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.Dictionary;

namespace Zek.Model.ViewModel.Person
{
    public class PersonsViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int Id { get; set; }

        [Display(Name = nameof(PersonResources.FullName), ResourceType = typeof(PersonResources))]
        public string FullName { get; set; }

        [Display(Name = nameof(PersonResources.PersonalNumber), ResourceType = typeof(PersonResources))]
        public string PersonalNumber { get; set; }


        [Display(Name = nameof(PersonResources.Passport), ResourceType = typeof(PersonResources))]
        public string Passport { get; set; }

        [Display(Name = nameof(PersonResources.Gender), ResourceType = typeof(PersonResources))]
        public Gender? GenderId;

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = nameof(PersonResources.BirthDate), ResourceType = typeof(PersonResources))]
        public DateTime? BirthDate { get; set; }

        //public AddressBaseViewModel Address { get; set; }

        [Display(Name = nameof(ApplicationResources.IsDeleted), ResourceType = typeof(ApplicationResources))]
        public bool IsDeleted { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Person
{
    public class PersonsPopupViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(PersonResources.FullName), ResourceType = typeof(PersonResources))]
        public string FullName { get; set; }

        [Display(Name = nameof(PersonResources.PersonalNumber), ResourceType = typeof(PersonResources))]
        public string PersonalNumber { get; set; }

        [Display(Name = nameof(PersonResources.Passport), ResourceType = typeof(PersonResources))]
        public string Passport { get; set; }
    }
}

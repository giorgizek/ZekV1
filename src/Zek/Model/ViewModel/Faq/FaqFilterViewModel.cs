using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Faq
{
    public class FaqFilterViewModel : FilterViewModel<FaqsViewModel>
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(ApplicationResources))]
        public string Name { get; set; }
    }
}

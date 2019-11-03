using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Pm
{
    public class PmFilterViewModel : FilterViewModel<PmsViewModel>
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Subject { get; set; }

        [Display(Name = nameof(EmailResources.Sent), ResourceType = typeof(EmailResources))]
        public bool Sent { get; set; }



    }
}

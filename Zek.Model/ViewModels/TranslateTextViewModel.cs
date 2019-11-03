using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels
{
    public class TranslateTextViewModel
    {
        [RequiredEx]
        [Display(Name = "CultureId", ResourceType = typeof(CultureResources))]
        public byte CultureId { get; set; }

        [Display(Name = "Language", ResourceType = typeof(CultureResources))]
        public string CultureName { get; set; }

        [RequiredEx]
        [StringLengthEx(255)]
        public string Text { get; set; }
    }
}

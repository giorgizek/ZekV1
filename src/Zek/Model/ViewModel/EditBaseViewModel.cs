using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zek.Localization;

namespace Zek.Model.ViewModel
{
    public interface IEditViewModel
    {
        int? Id { get; set; }
        bool ReadOnly { get; set; }
    }

    public class EditBaseViewModel : IEditViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [BindNever]
        public virtual bool ReadOnly { get; set; }
    }
}

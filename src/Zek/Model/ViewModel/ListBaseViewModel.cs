using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel
{

    public interface IListViewModel
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
    }


    public class ListBaseViewModel : IListViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int Id { get; set; }

        [Display(Name = nameof(ApplicationResources.IsDeleted), ResourceType = typeof(ApplicationResources))]
        public virtual bool IsDeleted { get; set; }
    }
}
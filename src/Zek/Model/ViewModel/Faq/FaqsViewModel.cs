using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zek.Localization;

namespace Zek.Model.ViewModel.Faq
{
    public class FaqsViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public int ApplicationId { get; set; }
        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public string Application { get; set; }

        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public int AreaId { get; set; }
        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public string Area { get; set; }

        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(ApplicationResources))]
        public string Name { get; set; }
    }


    public class FaqViewModel : EditBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public int? ApplicationId { get; set; }

        [BindNever]
        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public string Application { get; set; }

        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public int? AreaId { get; set; }

        [BindNever]
        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public string Area { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(400, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(ApplicationResources))]
        public string Name { get; set; }


        public List<FaqTranslateViewModel> Translates { get; set; }
    }

    public class FaqTranslateViewModel : EditBaseViewModel
    {
        public byte? CultureId { get; set; }

        public string Culture { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(400, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(FaqResources.Question), ResourceType = typeof(FaqResources))]
        public string Question { get; set; }

        [Display(Name = nameof(FaqResources.Answer), ResourceType = typeof(FaqResources))]
        public string Answer { get; set; }
    }
}

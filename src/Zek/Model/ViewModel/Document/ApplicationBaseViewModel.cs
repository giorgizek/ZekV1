using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Document
{
    public class ApplicationBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        //[Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [BindNever]
        [DateDisplayFormat(ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        [Display(Name = nameof(DocumentResources.ApplicationDate), ResourceType = typeof(DocumentResources))]
        public DateTime? Date { get; set; }

        [StringLength(400, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ApplicationResources.Comment), ResourceType = typeof(ApplicationResources))]
        public string Comment { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Attachment
{
    public class AttachmentBaseViewModel
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(AttachmentResources.FileName), ResourceType = typeof(AttachmentResources))]
        public string FileName { get; set; }
    }
}

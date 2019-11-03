using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Attachment
{
    public class AttachmentsViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public int? ApplicationId { get; set; }
        //[Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        //public string Application { get; set; }

        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public int? AreaId { get; set; }
        //[Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        //public string Area { get; set; }

        [Display(Name = nameof(AttachmentResources.FileName), ResourceType = typeof(AttachmentResources))]
        public string FileName { get; set; }

        [Display(Name = nameof(AttachmentResources.FileSize), ResourceType = typeof(AttachmentResources))]
        public long FileSize { get; set; }

        public Guid? RowGuid { get; set; }
    }

    public class AttachmentDataViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Application), ResourceType = typeof(ApplicationResources))]
        public int? ApplicationId { get; set; }

        [Display(Name = nameof(FaqResources.Area), ResourceType = typeof(FaqResources))]
        public int? AreaId { get; set; }

        [Display(Name = nameof(AttachmentResources.FileName), ResourceType = typeof(AttachmentResources))]
        public string FileName { get; set; }

        [Display(Name = nameof(AttachmentResources.FileData), ResourceType = typeof(AttachmentResources))]
        public byte[] FileData { get; set; }
    }
}

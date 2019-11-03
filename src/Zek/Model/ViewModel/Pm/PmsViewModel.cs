using System;
using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Pm
{
    public class PmsViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Subject { get; set; }

        [Display(Name = nameof(EmailResources.Body), ResourceType = typeof(EmailResources))]
        public string Body { get; set; }

        [DateTimeDisplayFormat]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime? Date { get; set; }

        public bool IsRead { get; set; }
        public bool IsReply { get; set; }
    }
}

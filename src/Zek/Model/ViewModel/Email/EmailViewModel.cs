using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.ViewModel.Attachment;

namespace Zek.Model.ViewModel.Email
{
    public class EmailViewModel
    {
        //public EmailViewModel()
        //{
        //    To = new List<EmailAddressViewModel>();
        //}
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.From), ResourceType = typeof(EmailResources))]
        public EmailAddressViewModel From { get; set; }

        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Subject { get; set; }

        [Display(Name = nameof(EmailResources.Body), ResourceType = typeof(EmailResources))]
        public string Body { get; set; }

        [Display(Name = nameof(EmailResources.IsHtml), ResourceType = typeof(EmailResources))]
        public bool IsHtml { get; set; }

        public List<EmailAttachmentViewModel> Attachments { get; set; }

        public List<EmailAddressViewModel> To { get; set; }
        public List<EmailAddressViewModel> Cc { get; set; }
        public List<EmailAddressViewModel> Bcc { get; set; }
    }
}

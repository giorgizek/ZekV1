using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Chat
{
    public class ChatViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [StringLength(400, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Name { get; set; }


        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public int? TypeId { get; set; }
        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public string Type { get; set; }

        [Display(Name = nameof(ChatResources.IsAnonymous), ResourceType = typeof(ChatResources))]
        public bool IsAnonymous { get; set; }

        [BindNever]
        [DateTimeDisplayFormat]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime? Date { get; set; }


        public List<MessagesViewModel> Messages { get; set; }


        public MessageViewModel Message { get; set; }

        [Display(Name = nameof(AttachmentResources.Attachments), ResourceType = typeof(AttachmentResources))]
        public List<string> Files { get; set; }
    }
}

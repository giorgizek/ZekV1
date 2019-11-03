using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Chat
{
    public class MessagesViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Subject { get; set; }

        [Display(Name = nameof(EmailResources.Body), ResourceType = typeof(EmailResources))]
        public string Body { get; set; }

        [DateTimeDisplayFormat]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime Date  { get; set; }

        public int CreatorId { get; set; }
        public string Creator { get; set; }
        public bool Sent { get; set; }
    }

    public class MessageFilterViewModel : FilterViewModel<MessagesViewModel>
    {
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        public int? ChatId { get; set; }
    }

    public class MessageViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        public int? ChatId { get; set; }
        public int? ParentId { get; set; }
        public int? FirstId { get; set; }


        [StringLength(400, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Subject), ResourceType = typeof(EmailResources))]
        public string Subject { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(5000, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(EmailResources.Body), ResourceType = typeof(EmailResources))]
        public string Body { get; set; }
    }


    public class CreateMessageViewModel : MessageViewModel
    {
        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public int? TypeId { get; set; }

        [BindNever]
        public SelectList Types { get; set; }

        [Display(Name = nameof(ChatResources.IsAnonymous), ResourceType = typeof(ChatResources))]
        public bool IsAnonymous { get; set; }


        //[FileExtensions(Extensions = "jpg,jpeg,png,pdf", ErrorMessageResourceName = nameof(DataAnnotationsResources.FileExtensionsAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        public List<IFormFile> Files { get; set; }
    }
}
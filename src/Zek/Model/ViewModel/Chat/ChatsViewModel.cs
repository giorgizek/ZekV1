using System;
using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Extensions;
using Zek.Localization;

namespace Zek.Model.ViewModel.Chat
{
    public class ChatsViewModel
    {

        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [DateDisplayFormat]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime Date { get; set; }

        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(EmailResources))]
        public string Name { get; set; }


        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public int? TypeId { get; set; }
        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public string Type { get; set; }

        [Display(Name = nameof(ChatResources.IsAnonymous), ResourceType = typeof(ChatResources))]
        public bool IsAnonymous { get; set; }

        public bool IsRead { get; private set; }
        private void InitIsRead()
        {
            IsRead = LastMessageId.IsEquals(LastReadMessageId);
        }
        
        //[Display(Name = nameof(EmailResources.Replied), ResourceType = typeof(EmailResources))]
        //public bool IsReply { get; set; }

        private int? _lastMessageId;
        public int? LastMessageId
        {
            get { return _lastMessageId; }
            set
            {
                if (!_lastMessageId.IsEquals(value))
                {
                    _lastMessageId = value;
                    InitIsRead();
                }
            }
        }

        [Display(Name = nameof(EmailResources.Mail), ResourceType = typeof(EmailResources))]
        public string LastMessage { get; set; }

        public int? CreateorId { get; set; }

        private int? _lastReadMessageId;
        public int? LastReadMessageId
        {
            get { return _lastReadMessageId; }
            set
            {
                if (!_lastReadMessageId.IsEquals(value))
                {
                    _lastReadMessageId = value;
                    InitIsRead();
                }
            }
        }
    }
}
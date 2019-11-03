using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zek.Localization;

namespace Zek.Model.ViewModel.Chat
{
    public class ChatFilterViewModel : FilterViewModel<ChatsViewModel>
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(ApplicationResources.Name), ResourceType = typeof(EmailResources))]
        public string Name { get; set; }

        [Display(Name = nameof(ApplicationResources.Type), ResourceType = typeof(ApplicationResources))]
        public int? TypeId { get; set; }
        [BindNever]
        public SelectList Types { get; set; }

        [BindNever]
        public int[] AnonymousTypeIds { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zek.Localization;

namespace Zek.Model.ViewModels
{
    public class MenuViewModel : DictionaryBaseViewModel
    {
        public MenuViewModel(Menu menu)
            : this()
        {
            Id = menu.Id;
            ParentId = menu.ParentId;

            ControllerName = menu.ControllerName;
            ActionName = menu.ActionName;
            Url = menu.Url;
            CreatorId = menu.CreatorId;
            CreateDate = menu.CreateDate;
            ModifierId = menu.ModifierId;
            ModifidDate = menu.ModifidDate;

            Texts = menu.Translates.Select(t => new TranslateTextViewModel { CultureId = t.CultureId, Text = t.Text }).ToList();
        }

        public MenuViewModel()
        {
            Children = new List<MenuViewModel>();
        }

        [Display(Name = "Id", ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = "ParentMenu", ResourceType = typeof(MenuResources))]
        public int? ParentId { get; set; }

        [Display(Name = "Name", ResourceType = typeof(MenuResources))]
        public string Name { get; set; }

        [Display(Name = "ControllerName", ResourceType = typeof(MenuResources))]
        public string ControllerName { get; set; }

        [Display(Name = "ActionName", ResourceType = typeof(MenuResources))]
        public string ActionName { get; set; }

        [Display(Name = "Url", ResourceType = typeof(MenuResources))]
        public string Url { get; set; }

        [Display(Name = "CreatorId", ResourceType = typeof(ApplicationResources))]
        //[UIHint("UserId")]
        public int CreatorId { get; set; }

        [Display(Name = "CreateDate", ResourceType = typeof(ApplicationResources))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "ModifierId", ResourceType = typeof(ApplicationResources))]
        //[UIHint("UserId")]
        public int? ModifierId { get; set; }

        [Display(Name = "ModifidDate", ResourceType = typeof(ApplicationResources))]
        public DateTime? ModifidDate { get; set; }

        public virtual ICollection<MenuViewModel> Children { get; set; }
    }
}

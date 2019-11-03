using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Utils;

namespace Zek.Model.ViewModel
{
    public class InfoViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(ApplicationResources.IsDeleted), ResourceType = typeof(ApplicationResources))]
        public bool IsDeleted { get; set; }

        [Display(Name = nameof(ApplicationResources.CreatorId), ResourceType = typeof(ApplicationResources))]
        public KeyPair<int?, string> Creator { get; set; }

        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(DateTimeResources.CreateDate), ResourceType = typeof(DateTimeResources))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = nameof(ApplicationResources.ModifierId), ResourceType = typeof(ApplicationResources))]
        public KeyPair<int?, string> Modifier { get; set; }

        [DisplayFormat(DataFormatString = "{0:G}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(DateTimeResources.ModifidDate), ResourceType = typeof(DateTimeResources))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = nameof(ApplicationResources.ApproverId), ResourceType = typeof(ApplicationResources))]
        public KeyPair<int?, string> Approver { get; set; }

        [Display(Name = nameof(DateTimeResources.ApproveDate), ResourceType = typeof(DateTimeResources))]
        public DateTime? ApproveDate { get; set; }
    }
}

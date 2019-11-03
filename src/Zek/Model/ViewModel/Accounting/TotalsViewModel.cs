using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.Accounting;

namespace Zek.Model.ViewModel.Accounting
{
    public class TotalsViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = nameof(AccountingResources.Amount), ResourceType = typeof(AccountingResources))]
        public decimal? Amount { get; set; }

        [Display(Name = nameof(AccountingResources.Currency), ResourceType = typeof(AccountingResources))]
        public ISO4217? Currency { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;
using Zek.Model.Accounting;

namespace Zek.Model.ViewModel.Accounting
{
    public class TransactionsBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [DateDisplayFormat]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime? Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = nameof(AccountingResources.Amount), ResourceType = typeof(AccountingResources))]
        public decimal? Amount { get; set; }

        [Display(Name = nameof(AccountingResources.Currency), ResourceType = typeof(AccountingResources))]
        public ISO4217? Currency { get; set; }
    }
}

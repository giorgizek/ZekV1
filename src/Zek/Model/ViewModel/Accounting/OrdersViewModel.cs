using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.Accounting;

namespace Zek.Model.ViewModel.Accounting
{
    public class OrdersViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(AccountingResources.OrderNumber), ResourceType = typeof(AccountingResources))]
        public string OrderNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = nameof(AccountingResources.GrandTotal), ResourceType = typeof(AccountingResources))]
        public decimal GrandTotal { get; set; }

        [Display(Name = nameof(AccountingResources.Currency), ResourceType = typeof(AccountingResources))]
        public ISO4217 Currency { get; set; }


        [Display(Name = nameof(ApplicationResources.Status), ResourceType = typeof(ApplicationResources))]
        public TransactionStatus StatusId { get; set; }

        [Display(Name = nameof(ApplicationResources.Status), ResourceType = typeof(ApplicationResources))]
        public string StatusName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
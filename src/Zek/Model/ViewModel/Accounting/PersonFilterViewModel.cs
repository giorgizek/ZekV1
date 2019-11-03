using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModel.Accounting
{
    public class OrderFilterViewModel : FilterViewModel<OrdersViewModel>
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(AccountingResources.OrderNumber), ResourceType = typeof(AccountingResources))]
        public string OrderNumber { get; set; }
    }

}

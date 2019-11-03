using System.ComponentModel.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.Accounting
{
    public enum PaymentMethod : byte
    {
        [Display(Name = nameof(Cash), ResourceType = typeof(AccountingResources))]
        Cash = 1,

        [Display(Name = nameof(CreditCard), ResourceType = typeof(AccountingResources))]
        CreditCard = 2,

        [Display(Name = nameof(BankTransfer), ResourceType = typeof(AccountingResources))]
        BankTransfer = 3,

        [Display(Name = nameof(Cheque), ResourceType = typeof(AccountingResources))]
        Cheque = 4,

        [Display(Name = nameof(Other), ResourceType = typeof(AccountingResources))]
        Other = 5
        //RevenueLoss,
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zek.Localization;
using Zek.Model.Accounting;

namespace Zek.Model.ViewModel.Accounting
{
    public class OrderViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [BindNever]
        //[Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(AccountingResources.OrderNumber), ResourceType = typeof(AccountingResources))]
        public string OrderNumber { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DataType(DataType.Date)]
        [Display(Name = nameof(DateTimeResources.Date), ResourceType = typeof(DateTimeResources))]
        public DateTime? Date { get; set; }


        //[Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(AccountingResources.Customer), ResourceType = typeof(AccountingResources))]
        public ButtonBrowseViewModel Customer { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Range(0, 1000000000000, ErrorMessageResourceName = nameof(DataAnnotationsResources.RangeAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(AccountingResources.Subtotal), ResourceType = typeof(AccountingResources))]
        public decimal? Subtotal { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Range(0, 1000000000000, ErrorMessageResourceName = nameof(DataAnnotationsResources.RangeAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Shipping { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Range(0, 1000000000000, ErrorMessageResourceName = nameof(DataAnnotationsResources.RangeAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? FreeShipping { get; set; }

        [BindNever]
        public decimal? TotalBeforeTax => Subtotal + Shipping - FreeShipping;


        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Range(0, 1000000000000, ErrorMessageResourceName = nameof(DataAnnotationsResources.RangeAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal? Tax { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = nameof(AccountingResources.GrandTotal), ResourceType = typeof(AccountingResources))]
        public decimal? GrandTotal => Subtotal + Shipping - FreeShipping + Tax;

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Range(1, short.MaxValue)]
        [Display(Name = nameof(AccountingResources.Currency), ResourceType = typeof(AccountingResources))]
        public ISO4217 CurrencyId { get; set; }

        [Display(Name = nameof(ApplicationResources.Status), ResourceType = typeof(ApplicationResources))]
        public TransactionStatus StatusId { get; set; }
    }
}

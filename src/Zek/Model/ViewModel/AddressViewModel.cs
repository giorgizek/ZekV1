using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zek.Localization;

namespace Zek.Model.ViewModel
{
    public class AddressBaseViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        /// <summary>
        /// Country ID (<see cref="https://en.wikipedia.org/wiki/ISO_3166-1">ISO 3166</see>)
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Country), ResourceType = typeof(ContactResources))]
        public int? CountryId { get; set; }
        [Display(Name = nameof(ContactResources.Country), ResourceType = typeof(ContactResources))]
        public string Country { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.City), ResourceType = typeof(ContactResources))]
        public int? CityId { get; set; }
        [Display(Name = nameof(ContactResources.City), ResourceType = typeof(ContactResources))]
        public string City { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.Street), ResourceType = typeof(ContactResources))]
        public string Street { get; set; }

        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.HouseNumber), ResourceType = typeof(ContactResources))]
        public string HouseNumber { get; set; }

        [StringLength(25, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(ContactResources.PostalCode), ResourceType = typeof(ContactResources))]
        public string PostalCode { get; set; }
    }

    public class AddressViewModel : AddressBaseViewModel
    {
        [BindNever]
        public SelectList Countries { get; set; }

        [BindNever]
        public SelectList Cities { get; set; }
    }
}

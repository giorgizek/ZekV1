using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Localization;

namespace Zek.Model.ViewModels
{
    public class AddressViewModel
    {
        [Display(Name = "Id", ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [RequiredEx]
        [Display(Name = "Country", ResourceType = typeof(ContactResources))]
        public short CountryId { get; set; }
        [Display(Name = "Country", ResourceType = typeof(ContactResources))]
        public string Country { get; set; }

        [RequiredEx]
        [Display(Name = "City", ResourceType = typeof(ContactResources))]
        public int CityId { get; set; }
        [Display(Name = "City", ResourceType = typeof(ContactResources))]
        public string City { get; set; }



        [RequiredEx]
        [Display(Name = "Street", ResourceType = typeof(ContactResources))]
        public string Street { get; set; }

        [RequiredEx]
        [StringLengthEx(25)]
        [Display(Name = "HouseNumber", ResourceType = typeof(ContactResources))]
        public string HouseNumber { get; set; }

        [StringLengthEx(25)]
        [Display(Name = "PostalCode", ResourceType = typeof(ContactResources))]
        public string PostalCode { get; set; }
    }
}

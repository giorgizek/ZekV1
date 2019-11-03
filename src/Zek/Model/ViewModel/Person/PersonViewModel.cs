using System;
using System.ComponentModel.DataAnnotations;
using Zek.Localization;
using Zek.Model.Dictionary;
using Zek.Model.ViewModel.Contact;

namespace Zek.Model.ViewModel.Person
{
    public class PersonViewModel
    {
        [Display(Name = nameof(ApplicationResources.Id), ResourceType = typeof(ApplicationResources))]
        public int? Id { get; set; }

        [Display(Name = nameof(PersonResources.Title), ResourceType = typeof(PersonResources))]
        public byte? TitleId { get; set; }

        [Display(Name = nameof(PersonResources.Title), ResourceType = typeof(PersonResources))]
        public string Title { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(100, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.FirstName), ResourceType = typeof(PersonResources))]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [StringLength(150, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.LastName), ResourceType = typeof(PersonResources))]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.FirstNameEn), ResourceType = typeof(PersonResources))]
        public string FirstNameEn { get; set; }

        [StringLength(150, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.LastnameEn), ResourceType = typeof(PersonResources))]
        public string LastNameEn { get; set; }

        /// <summary>
        /// Personal number or ID card number
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.PersonalNumber), ResourceType = typeof(PersonResources))]
        public string PersonalNumber { get; set; }

        /// <summary>
        /// Passport
        /// </summary>
        [StringLength(50, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        [Display(Name = nameof(PersonResources.Passport), ResourceType = typeof(PersonResources))]
        public string Passport { get; set; }

        /// <summary>
        /// Gender
        /// </summary>
        [Display(Name = nameof(PersonResources.Gender), ResourceType = typeof(PersonResources))]
        public Gender? GenderId { get; set; }

        /// <summary>
        /// Birth date
        /// </summary>
        [DataType(DataType.Date)]
        [Display(Name = nameof(PersonResources.BirthDate), ResourceType = typeof(PersonResources))]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Delete flag
        /// </summary>
        [Display(Name = nameof(ApplicationResources.IsDeleted), ResourceType = typeof(ApplicationResources))]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Display(Name = nameof(ContactResources.Address), ResourceType = typeof(ContactResources))]
        public AddressViewModel Address { get; set; }

        /// <summary>
        /// Contact
        /// </summary>
        public ContactViewModel Contact { get; set; }
    }
}

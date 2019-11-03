using System;
using System.ComponentModel.DataAnnotations;
using Zek.DataAnnotations;
using Zek.Extensions;
using Zek.Localization;

namespace Zek.Model.ViewModels
{
    public class DayOffViewModel
    {
        public DayOffViewModel()
        {
            Date = DateTime.Now.Date;
            FromDate = DateTime.Now.Date.AddBusinessDays(0);
            ToDate = DateTime.Now.Date.AddBusinessDays(1);
            Days = 1;
        }


        [RequiredEx]
        [Display(Name = "FirstName", ResourceType = typeof(PersonResources))]
        public string FirstName { get; set; }

        [RequiredEx]
        [Display(Name = "LastName", ResourceType = typeof(PersonResources))]
        public string LastName { get; set; }

        [RequiredEx]
        [RangeEx(0.5, 2)]
        [Display(Name = "DayOffDays", ResourceType = typeof(HRResources))]
        [DataType(DataType.Currency), DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal Days { get; set; }

        [RequiredEx]
        [Display(Name = "DayOffFromDate", ResourceType = typeof(HRResources))]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [RequiredEx]
        [Display(Name = "DayOffToDate", ResourceType = typeof(HRResources))]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [RequiredEx]
        [Display(Name = "Date", ResourceType = typeof(ApplicationResources))]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [RequiredEx]
        [Display(Name = "HeadFirstName", ResourceType = typeof(HRResources))]
        public string HeadFirstName { get; set; }

        [RequiredEx]
        [Display(Name = "HeadLastName", ResourceType = typeof(HRResources))]
        public string HeadLastName { get; set; }
    }
}

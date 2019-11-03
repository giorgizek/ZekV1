using System;
using System.Collections.Generic;
using Zek.Extensions;
using Zek.Properties;
using Zek.Utils;

namespace Zek.Core
{
    [Serializable]
    public enum RoundingDirection
    {
        RoundUp,
        RoundDown,
        Round
    }


    [Serializable]
    public enum SqlDayOfWeek
    {
        /// <summary>
        /// კვირა.
        /// </summary>
        Sunday = 1,
        /// <summary>
        /// ორშაბათი
        /// </summary>
        Monday = 2,
        /// <summary>
        /// სამშაბათი
        /// </summary>
        Tuesday = 3,
        /// <summary>
        /// ოთხშაბათი
        /// </summary>
        Wednesday = 4,
        /// <summary>
        /// ხუთშაბათი
        /// </summary>
        Thursday = 5,
        /// <summary>
        /// პარასკევი
        /// </summary>
        Friday = 6,
        /// <summary>
        /// შაბათი
        /// </summary>
        Saturday = 7,
    }

    /// <summary>
    /// კვირების რაოდენობა ერთ თვეში.
    /// </summary>
    [Serializable]
    public enum WeekOfMonth
    {
        None = 0,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 5
    }



    /// <summary>
    /// კვირის დღის ბინალური ენუმერატორი.
    /// კვირა=1,2,4,8,16,32,64.
    /// </summary>
    [Flags]
    [Serializable]
    public enum WeekDays
    {
        /// <summary>
        /// ცარიელი
        /// </summary>
        None = 0,
        /// <summary>
        /// კვირა
        /// </summary>
        Sunday = 1,
        /// <summary>
        /// ორშაბათი
        /// </summary>
        Monday = 2,
        /// <summary>
        /// სამშაბათი
        /// </summary>
        Tuesday = 4,
        /// <summary>
        /// ოთხშაბათი
        /// </summary>
        Wednesday = 8,
        /// <summary>
        /// ხუთშაბათი
        /// </summary>
        Thursday = 16,
        /// <summary>
        /// პარასკევი
        /// </summary>
        Friday = 32,
        /// <summary>
        /// შაბათი
        /// </summary>
        Saturday = 64,

        /// <summary>
        /// შაბათი, კვირა
        /// </summary>
        WeekendDays = Sunday | Saturday,
        /// <summary>
        /// სამუშაო დთეები. (ორშაბათი-პარასკევი)
        /// </summary>
        WorkDays = Monday | Tuesday | Wednesday | Thursday | Friday,
        /// <summary>
        /// კვირის ყველა დღე
        /// </summary>
        EveryDay = WeekendDays | WorkDays
    }

    /// <summary>
    /// კვარტლის ენუმერატორი.
    /// </summary>
    [Serializable]
    public enum Quarter
    {
        /// <summary>
        /// პირველი კვარტალი
        /// </summary>
        First = 1,
        /// <summary>
        /// მეორე კვარტალი
        /// </summary>
        Second = 2,
        /// <summary>
        /// მესამე კვარტალი
        /// </summary>
        Third = 3,
        /// <summary>
        /// მეოთხე კვარტალი
        /// </summary>
        Fourth = 4
    }

    /// <summary>
    /// თვის ენუმერატორი.
    /// </summary>
    [Serializable]
    public enum Month
    {
        /// <summary>
        /// იანვარი
        /// </summary>
        January = 1,
        /// <summary>
        /// თებერვალი
        /// </summary>
        February = 2,
        /// <summary>
        /// მარტი
        /// </summary>
        March = 3,
        /// <summary>
        /// აპრილი
        /// </summary>
        April = 4,
        /// <summary>
        /// მაისი
        /// </summary>
        May = 5,
        /// <summary>
        /// ივნისი
        /// </summary>
        June = 6,
        /// <summary>
        /// ივლისი
        /// </summary>
        July = 7,
        /// <summary>
        /// აგვისტო
        /// </summary>
        August = 8,
        /// <summary>
        /// სექტემბერი
        /// </summary>
        September = 9,
        /// <summary>
        /// ოქრომბერი
        /// </summary>
        October = 10,
        /// <summary>
        /// ნოემბერი
        /// </summary>
        November = 11,
        /// <summary>
        /// დეკემბერი
        /// </summary>
        December = 12
    }

    public class DateTimeHelper
    {
        #region Fields
        /// <summary>
        /// მინიმალური თარიღი.
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 1/1/1900</remarks>
        public static DateTime MinDate => DateTimeExtensions.MinDate;

        /// <summary>
        /// მინიმალური ვალიდური თარიღი. 
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 1/1/1980</remarks>
        public static DateTime EarliestDate => DateTimeExtensions.EarliestDate;

        /// <summary>
        /// მაქსიმალური თარიღი.
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 12/31/9000</remarks>
        public static DateTime MaxDate => DateTimeExtensions.MaxDate;

        #endregion

        #region Seconds
        public static DateTime GetStartOfSecond(object value)
        {
            return ConvertHelper.ToDateTime(value).GetStartOfSecond();
        }
        public static DateTime GetStartOfSecond(string value)
        {
            return value.ParseDateTime().GetStartOfSecond();
        }

        public static DateTime GetEndOfSecond(object value)
        {
            return ConvertHelper.ToDateTime(value).GetEndOfSecond();
        }
        public static DateTime GetEndOfSecond(string value)
        {
            return value.ParseDateTime().GetEndOfSecond();
        }

        #endregion

        #region Minutes
        public static DateTime GetStartOfMinute(object value)
        {
            return ConvertHelper.ToDateTime(value).GetStartOfMinute();
        }
        public static DateTime GetStartOfMinute(string value)
        {
            return value.ParseDateTime().GetStartOfMinute();
        }

        public static DateTime GetEndOfMinute(object value)
        {
            return ConvertHelper.ToDateTime(value).GetEndOfMinute();
        }
        public static DateTime GetEndOfMinute(string value)
        {
            return value.ParseDateTime().GetEndOfMinute();
        }
        #endregion

        #region Hours
        public static DateTime GetStartOfHour(object value)
        {
            return ConvertHelper.ToDateTime(value).GetStartOfHour();
        }
        public static DateTime GetStartOfHour(string value)
        {
            return value.ParseDateTime().GetStartOfHour();
        }

        public static DateTime GetEndOfHour(object value)
        {
            return ConvertHelper.ToDateTime(value).GetEndOfHour();
        }
        public static DateTime GetEndOfHour(string value)
        {
            return value.ParseDateTime().GetEndOfHour();
        }
        #endregion

        #region Weeks
        public static DateTime GetStartOfLastWeek()
        {
            var daysToSubtract = (int)DateTime.Now.DayOfWeek + 7;
            var dt = DateTime.Now.Subtract(TimeSpan.FromDays(daysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfLastWeek()
        {
            var dt = GetStartOfLastWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }
        public static DateTime GetStartOfCurrentWeek()
        {
            var faysToSubtract = (int)DateTime.Now.DayOfWeek;
            var dt = DateTime.Now.Subtract(TimeSpan.FromDays(faysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfCurrentWeek()
        {
            var dt = GetStartOfCurrentWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }
        #endregion

        #region Months
        public static DateTime GetStartOfMonth(object value)
        {
            return ConvertHelper.ToDateTime(value).GetStartOfMonth();
        }
        public static DateTime GetStartOfMonth(string value)
        {
            return value.ParseDateTime().GetStartOfMonth();
        }

        public static DateTime GetStartOfMonth(Month month, int year)
        {
            return GetStartOfMonth((int)month, year);
        }
        public static DateTime GetStartOfMonth(int month, int year)
        {
            return new DateTime(year, month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(Month month, int year)
        {
            return GetEndOfMonth(year, (int)month);
        }
        public static DateTime GetEndOfMonth(int month, int year)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetStartOfMonth(12, DateTime.Now.Year - 1);
            return GetStartOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        public static DateTime GetEndOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetEndOfMonth(12, DateTime.Now.Year - 1);
            return GetEndOfMonth(DateTime.Now.Month - 1, DateTime.Now.Year);
        }

        public static DateTime GetStartOfCurrentMonth()
        {
            return GetStartOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentMonth()
        {
            return GetEndOfMonth(DateTime.Now.Month, DateTime.Now.Year);
        }
        #endregion

        #region Quarters
        public static DateTime GetStartOfQuarter(int year, Quarter qtr)
        {
            switch (qtr)
            {
                case Quarter.First:// 1st Quarter = January 1 to March 31
                    return new DateTime(year, 1, 1, 0, 0, 0, 0);

                case Quarter.Second:// 2nd Quarter = April 1 to June 30
                    return new DateTime(year, 4, 1, 0, 0, 0, 0);

                case Quarter.Third:// 3rd Quarter = July 1 to September 30
                    return new DateTime(year, 7, 1, 0, 0, 0, 0);

                case Quarter.Fourth:// 4th Quarter = October 1 to December 31
                    return new DateTime(year, 10, 1, 0, 0, 0, 0);
            }

            throw new ArgumentException(Resources.InvalidQuarterErrorText, nameof(qtr));
        }
        public static DateTime GetEndOfQuarter(int year, Quarter qtr)
        {
            switch (qtr)
            {
                case Quarter.First:// 1st Quarter = January 1 to March 31
                    return new DateTime(year, 3, DateTime.DaysInMonth(year, 3), 23, 59, 59, 999);

                case Quarter.Second:// 2nd Quarter = April 1 to June 30
                    return new DateTime(year, 6, DateTime.DaysInMonth(year, 6), 23, 59, 59, 999);

                case Quarter.Third:// 3rd Quarter = July 1 to September 30
                    return new DateTime(year, 9, DateTime.DaysInMonth(year, 9), 23, 59, 59, 999);

                case Quarter.Fourth:// 4th Quarter = October 1 to December 31
                    return new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
            }

            throw new ArgumentException(Resources.InvalidQuarterErrorText, nameof(qtr));
        }
        public static Quarter GetQuarter(Month month)
        {
            if (month <= Month.March)
                // 1st Quarter = January 1 to March 31
                return Quarter.First;

            if ((month >= Month.April) && (month <= Month.June))
                // 2nd Quarter = April 1 to June 30
                return Quarter.Second;

            if ((month >= Month.July) && (month <= Month.September))
                // 3rd Quarter = July 1 to September 30
                return Quarter.Third;

            // 4th Quarter = October 1 to December 31
            return Quarter.Fourth;
        }

        public static DateTime GetEndOfLastQuarter()
        {
            if ((Month)DateTime.Now.Month <= Month.March)
                //go to last quarter of previous year
                return GetEndOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth);

            return GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfLastQuarter()
        {
            if ((Month)DateTime.Now.Month <= Month.March)
                //go to last quarter of previous year
                return GetStartOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth);

            return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }

        public static DateTime GetStartOfCurrentQuarter()
        {
            return GetStartOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }
        public static DateTime GetEndOfCurrentQuarter()
        {
            return GetEndOfQuarter(DateTime.Now.Year, GetQuarter((Month)DateTime.Now.Month));
        }
        #endregion

        #region Years
        public static DateTime GetStartOfYear(int year)
        {
            return new DateTime(year, 1, 1, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfYear(int year)
        {
            return new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
        }
        public static DateTime GetStartOfLastYear()
        {
            return GetStartOfYear(DateTime.Now.Year - 1);
        }
        public static DateTime GetEndOfLastYear()
        {
            return GetEndOfYear(DateTime.Now.Year - 1);
        }
        public static DateTime GetStartOfCurrentYear()
        {
            return GetStartOfYear(DateTime.Now.Year);
        }
        public static DateTime GetEndOfCurrentYear()
        {
            return GetEndOfYear(DateTime.Now.Year);
        }
        #endregion


        public static bool Overlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 >= start2 && start1 <= end2) || (end1 >= start2 && end1 <= end2) || (start1 <= start2 && end1 >= end2);
        }
        public static bool NotOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return (start1 > end2) || (end1 < start2);
        }
       

        #region DateToStr
        /// <summary>
        /// ბრუნვები (გამოიყენება თარიღის კონვერტაციისას).
        /// </summary>
        [Serializable]
        public enum CaseNames
        {
            /// <summary>
            /// სახელობითი.
            /// </summary>
            Nominative,
            /// <summary>
            /// მიცემითი.
            /// </summary>
            Dative,
        }

        //private static readonly string[] _months = new string[]
        //{
        //    "იანვარი", "თებერვალი", "მარტი", "აპრილი", "მაისი", "ივნისი",
        //    "ივლისი", "აგვისტო", "სექტემბერი", "ოქტომბერი", "ნოემბერი", "დეკემბერი"
        //};
        //private static readonly string[] _weekdays = new string[]
        //{
        //    "კვირა", "ორშაბათი", "სამშაბათი", "ოთხშაბათი",
        //    "ხუთშაბათი", "პარასკევი", "შაბათი",
        //};

        /// <summary>
        /// გადაყავის სიტყვა ბრუნვაში
        /// </summary>
        /// <param name="word">სიტყვა</param>
        /// <param name="caseName">ბრუნვა</param>
        /// <returns>გადაყვანილი სიტყვა</returns>
        public static string CombineWordCase(string word, CaseNames caseName)
        {
            if (caseName == CaseNames.Nominative) return word;

            return $"{(word.EndsWith("ი") ? word.Substring(0, word.Length - 1) : word)}ს";
        }

        /// <summary>
        /// აბრუნებს თარიღს სიტყვიერად.
        /// </summary>
        /// <param name="date">თარიღი რომლის გადაყვანაც გვინდა.</param>
        /// <returns>აბრუნებს სიტყვიერად გამოსხულ თარიღს.</returns>
        public static string DateToStr(DateTime date)
        {
            return DateToStr(date, CaseNames.Nominative);
        }
        /// <summary>
        /// აბრუნებს თარიღს სიტყვიერად.
        /// </summary>
        /// <param name="date">თარიღი რომლის გადაყვანაც გვინდა.</param>
        /// <param name="caseName">ბრუნვის დასახელება.</param>
        /// <returns>აბრუნებს სიტყვიერად გამოსხულ თარიღს.</returns>
        public static string DateToStr(DateTime date, CaseNames caseName)
        {
            return $"{date.Day} {CombineWordCase(GetMonthName(date.Month), caseName)} {date.Year}";
        }

        public static string GetMonthName(DateTime? date)
        {
            return date != null ? GetMonthName(date.Value.Month) : null;
        }
        public static string GetMonthName(DateTime date)
        {
            return GetMonthName(date.Month);
        }
        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "იანვარი";
                case 2:
                    return "თებერვალი";
                case 3:
                    return "მარტი";
                case 4:
                    return "აპრილი";
                case 5:
                    return "მაისი";
                case 6:
                    return "ივნისი";
                case 7:
                    return "ივლისი";
                case 8:
                    return "აგვისტო";
                case 9:
                    return "სექტემბერი";
                case 10:
                    return "ოქტომბერი";
                case 11:
                    return "ნოემბერი";
                case 12:
                    return "დეკემბერი";
                default:
                    throw new ArgumentException(Resources.InvalidMonthErrorText, nameof(month));
            }
        }

        public static string DateToMonth(DateTime date)
        {
            return DateToMonth(date, CaseNames.Nominative);
        }
        public static string DateToMonth(DateTime date, CaseNames caseName)
        {
            return CombineWordCase(GetMonthName(date.Month), caseName);
        }
        public static string DayOfWeek(DateTime date)
        {
            return DayOfWeek(date, CaseNames.Nominative);
        }
        public static string DayOfWeek(DateTime date, CaseNames caseName)
        {
            return DayOfWeek(date.DayOfWeek, caseName);
        }
        public static string DayOfWeek(WeekDays dayOfWeekFlag)
        {
            return DayOfWeek(dayOfWeekFlag, CaseNames.Nominative);
        }
        public static string DayOfWeek(WeekDays dayOfWeekFlag, CaseNames caseName)
        {
            switch (dayOfWeekFlag)
            {
                case WeekDays.Sunday:
                    return DayOfWeek(System.DayOfWeek.Sunday, caseName);

                case WeekDays.Monday:
                    return DayOfWeek(System.DayOfWeek.Monday, caseName);

                case WeekDays.Tuesday:
                    return DayOfWeek(System.DayOfWeek.Tuesday, caseName);

                case WeekDays.Wednesday:
                    return DayOfWeek(System.DayOfWeek.Wednesday, caseName);

                case WeekDays.Thursday:
                    return DayOfWeek(System.DayOfWeek.Thursday, caseName);

                case WeekDays.Friday:
                    return DayOfWeek(System.DayOfWeek.Friday, caseName);

                case WeekDays.Saturday:
                    return DayOfWeek(System.DayOfWeek.Saturday, caseName);

                default:
                    return DayOfWeek(System.DayOfWeek.Sunday, caseName);
            }
        }
        public static string DayOfWeek(DayOfWeek dayOfWeek)
        {
            return DayOfWeek(dayOfWeek, CaseNames.Nominative);
        }
        public static string DayOfWeek(DayOfWeek dayOfWeek, CaseNames caseName)
        {
            return CombineWordCase(GetWeekDay((int)dayOfWeek), caseName);
        }

        /// <summary>
        /// აბრუნებს კვირის დღეებს ქართულად.
        /// </summary>
        /// <param name="day">კვირის დღე 0=კვირა, 1=ორშაბათი...</param>
        /// <returns>აბრუნებს კვირის დღეებს ქართულად.</returns>
        public static string GetWeekDay(int day)
        {
            switch (day)
            {
                case 0:
                    return "კვირა";
                case 1:
                    return "ორშაბათი";
                case 2:
                    return "სამშაბათი";
                case 3:
                    return "ოთხშაბათი";
                case 4:
                    return "ხუთშაბათი";
                case 5:
                    return "პარასკევი";
                case 6:
                    return "შაბათი";
                default:
                    throw new ArgumentException(Resources.InvalidDayErrorText, nameof(day));
            }
        }


        public static bool HasFlag(WeekDays dayOfWeekFlags, WeekDays dayOfWeekFlagToCheck)
        {
            return BitwiseHelper.HasFlag((int)dayOfWeekFlags, (int)dayOfWeekFlagToCheck);
        }
        public static string DaysOfWeek(WeekDays dayOfWeekFlag)
        {
            return DaysOfWeek(dayOfWeekFlag, ", ");
        }
        public static string DaysOfWeek(WeekDays dayOfWeekFlag, string separator)
        {
            return string.Join(separator, DaysOfWeekArray(dayOfWeekFlag));
        }
        public static string[] DaysOfWeekArray(WeekDays dayOfWeekFlag)
        {
            var result = new List<string>(7);

            if (HasFlag(dayOfWeekFlag, WeekDays.Sunday))
                result.Add(DayOfWeek(WeekDays.Sunday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Monday))
                result.Add(DayOfWeek(WeekDays.Monday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Tuesday))
                result.Add(DayOfWeek(WeekDays.Tuesday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Wednesday))
                result.Add(DayOfWeek(WeekDays.Wednesday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Thursday))
                result.Add(DayOfWeek(WeekDays.Thursday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Friday))
                result.Add(DayOfWeek(WeekDays.Friday));

            if (HasFlag(dayOfWeekFlag, WeekDays.Saturday))
                result.Add(DayOfWeek(WeekDays.Saturday));

            return result.ToArray();
        }
        #endregion


        /// <summary>
        /// Converts a fractional hour value like 1.25 to 1:15  hours:minutes format
        /// </summary>
        /// <param name="hours">Decimal hour value</param>
        /// <param name="format">An optional format string where {0} is hours and {1} is minutes.</param>
        /// <returns></returns>
        public static string FractionalHoursToString(decimal hours, string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "{0}:{1}";

            var tspan = TimeSpan.FromHours((double)hours);
            return string.Format(format, tspan.Hours, tspan.Minutes);
        }
        /// <summary>
        /// Converts a fractional hour value like 1.25 to 1:15  hours:minutes format
        /// </summary>
        /// <param name="hours">Decimal hour value</param>
        public static string FractionalHoursToString(decimal hours)
        {
            return FractionalHoursToString(hours, null);
        }
        /// <summary>
        /// Displays a long date in friendly notation
        /// </summary>
        /// <param name="date"></param>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public static string FriendlyDateString(DateTime date, bool showTime)
        {

            string formattedDate;
            if (date.Date == DateTime.Today)
                formattedDate = "Today"; //Resources.Resources.Today; 
            else if (date.Date == DateTime.Today.AddDays(-1))
                formattedDate = "Yesterday"; //Resources.Resources.Yesterday;
            else if (date.Date > DateTime.Today.AddDays(-6))
                // *** Show the Day of the week
                formattedDate = date.ToString("dddd");
            else
                formattedDate = date.ToString("MMMM dd, yyyy");

            if (showTime)
                formattedDate += $" @ {date.ToString("t").ToLowerInvariant()}";

            return formattedDate;
        }
        /// <summary>
        /// Rounds an hours value to a minute interval
        /// 0 means no rounding
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minuteInterval">Minutes to round up or down to</param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static decimal RoundDateToMinuteInterval(decimal hours, int minuteInterval, RoundingDirection direction)
        {
            if (minuteInterval == 0)
                return hours;

            var fraction = 60m / minuteInterval;

            switch (direction)
            {
                case RoundingDirection.Round:
                    return Math.Round(hours * fraction, 0) / fraction;

                case RoundingDirection.RoundDown:
                    return Math.Truncate(hours * fraction) / fraction;

                default:
                    return Math.Ceiling(hours * fraction) / fraction;
            }
        }
        /// <summary>
        /// Rounds a date value to a given minute interval
        /// </summary>
        /// <param name="time">Original time value</param>
        /// <param name="minuteInterval">Number of minutes to round up or down to</param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static DateTime RoundDateToMinuteInterval(DateTime time, int minuteInterval, RoundingDirection direction)
        {
            if (minuteInterval == 0)
                return time;

            decimal interval = minuteInterval;
            var actMinute = (decimal)time.Minute;

            if (actMinute == 0.00M)
                return time;

            var newMinutes = 0;

            switch (direction)
            {
                case RoundingDirection.Round:
                    newMinutes = (int)(Math.Round(actMinute / interval, 0) * interval);
                    break;

                case RoundingDirection.RoundDown:
                    newMinutes = (int)(Math.Truncate(actMinute / interval) * interval);
                    break;

                case RoundingDirection.RoundUp:
                    newMinutes = (int)(Math.Ceiling(actMinute / interval) * interval);
                    break;
            }


            // *** strip time 
            time = time.AddMinutes(time.Minute * -1);
            time = time.AddSeconds(time.Second * -1);
            time = time.AddMilliseconds(time.Millisecond * -1);

            // *** add new minutes back on            
            return time.AddMinutes(newMinutes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Zek.Extensions
{
    [Serializable]
    public enum DateTimePrecision
    {
        Days,
        Hours,
        Minutes,
        Seconds,
        Milliseconds,
    }

    public static class DateTimeExtensions
    {

        #region Fields
        /// <summary>
        /// მინიმალური თარიღი.
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 1/1/1900</remarks>
        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        /// <summary>
        /// მინიმალური ვალიდური თარიღი. 
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 1/1/1980</remarks>
        public static readonly DateTime EarliestDate = new DateTime(1980, 1, 1);
        /// <summary>
        /// მაქსიმალური თარიღი.
        /// </summary>
        /// <remarks>მისი მნიშვნელობაა: 12/31/9000</remarks>
        public static readonly DateTime MaxDate = new DateTime(9000, 12, 31);
        #endregion


    

        /// <summary>
        /// Converts datetime to int (SQL: SELECT CAST(@date AS INT)).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int ToSQLInt32(this DateTime date)
        {
            return (int)(date.Date - new DateTime(1900, 1, 1)).TotalDays;
        }

        /// <summary>
        /// Converts int to datetime (SQL: SELECT CAST(@date AS DATETIME)).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToSQLDateTime(this int date)
        {
            return new DateTime(1900, 1, 1).AddDays(date);
        }

    

        public static DateTime TrimTo(this DateTime dateTime, DateTimePrecision precision)
        {
            switch (precision)
            {
                case DateTimePrecision.Days: return dateTime.Date;
                case DateTimePrecision.Hours: return TrimToHours(dateTime);
                case DateTimePrecision.Minutes: return TrimToMinutes(dateTime);
                case DateTimePrecision.Seconds: return TrimToSeconds(dateTime);
                case DateTimePrecision.Milliseconds: return dateTime;
            }
            throw new ArgumentException("precission");
        }
        public static DateTime TrimToSeconds(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }
        public static DateTime TrimToMinutes(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, dateTime.Kind);
        }
        public static DateTime TrimToHours(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
        }
        public static DateTimePrecision GetPrecision(this DateTime dateTime)
        {
            if (dateTime.Millisecond != 0)
                return DateTimePrecision.Milliseconds;
            if (dateTime.Second != 0)
                return DateTimePrecision.Seconds;
            if (dateTime.Minute != 0)
                return DateTimePrecision.Minutes;
            if (dateTime.Hour != 0)
                return DateTimePrecision.Hours;

            return DateTimePrecision.Days;
        }
        public static TimeSpan TrimTo(this TimeSpan timeSpan, DateTimePrecision precision)
        {
            switch (precision)
            {
                case DateTimePrecision.Days: return timeSpan.TrimToDays();
                case DateTimePrecision.Hours: return TrimToHours(timeSpan);
                case DateTimePrecision.Minutes: return TrimToMinutes(timeSpan);
                case DateTimePrecision.Seconds: return TrimToSeconds(timeSpan);
                case DateTimePrecision.Milliseconds: return timeSpan;
            }
            throw new ArgumentException("precission");
        }
        public static TimeSpan TrimToSeconds(this TimeSpan dateTime)
        {
            return new TimeSpan(dateTime.Days, dateTime.Hours, dateTime.Minutes, dateTime.Seconds);
        }
        public static TimeSpan TrimToMinutes(this TimeSpan dateTime)
        {
            return new TimeSpan(dateTime.Days, dateTime.Hours, dateTime.Minutes, 0);
        }
        public static TimeSpan TrimToHours(this TimeSpan dateTime)
        {
            return new TimeSpan(dateTime.Days, dateTime.Hours, 0, 0);
        }
        public static TimeSpan TrimToDays(this TimeSpan dateTime)
        {
            return new TimeSpan(dateTime.Days, 0, 0, 0);
        }
        public static DateTimePrecision? GetPrecision(this TimeSpan timeSpan)
        {
            if (timeSpan.Milliseconds != 0)
                return DateTimePrecision.Milliseconds;
            if (timeSpan.Seconds != 0)
                return DateTimePrecision.Seconds;
            if (timeSpan.Minutes != 0)
                return DateTimePrecision.Minutes;
            if (timeSpan.Hours != 0)
                return DateTimePrecision.Hours;
            if (timeSpan.Days != 0)
                return DateTimePrecision.Days;

            return null;
        }
        public static string NiceToString(this TimeSpan timeSpan, DateTimePrecision precission = DateTimePrecision.Milliseconds)
        {
            var sb = new StringBuilder();
            var any = false;
            if (timeSpan.Days != 0 && precission >= DateTimePrecision.Days)
            {
                sb.AppendFormat("{0}d ", timeSpan.Days);
                any = true;
            }

            if ((any || timeSpan.Hours != 0) && precission >= DateTimePrecision.Hours)
            {
                sb.AppendFormat("{0,2}h ", timeSpan.Hours);
                any = true;
            }

            if ((any || timeSpan.Minutes != 0) && precission >= DateTimePrecision.Minutes)
            {
                sb.AppendFormat("{0,2}m ", timeSpan.Minutes);
                any = true;
            }

            if ((any || timeSpan.Seconds != 0) && precission >= DateTimePrecision.Seconds)
            {
                sb.AppendFormat("{0,2}s ", timeSpan.Seconds);
                any = true;
            }

            if ((any || timeSpan.Milliseconds != 0) && precission >= DateTimePrecision.Milliseconds)
            {
                sb.AppendFormat("{0,3}ms", timeSpan.Milliseconds);
            }

            return sb.ToString();
        }
        public static long JavascriptMilliseconds(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
                throw new InvalidOperationException("dateTime should be UTC");

            return (long)new TimeSpan(dateTime.Ticks - new DateTime(1970, 1, 1).Ticks).TotalMilliseconds;
        }



        public static string ToJavaScriptString(this DateTime date)
        {
            return new DateTime?(date).ToJavaScriptString();
        }
        public static string ToJavaScriptString(this DateTime? date)
        {
            return date?.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty;
        }


        /// <summary>
        /// უცვლის დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="time">თარიღი, რომლიდანაც ამოვიღებთ მხოლოდ დროს.</param>
        /// <returns>აბრუნებს შეცვლილ თარიღს.</returns>
        public static DateTime CombineEarliestDateTime(this DateTime? time)
        {
            return time != null ? CombineEarliestDateTime(time.Value) : MinDate;
        }
        /// <summary>
        /// უცვლის დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="time">თარიღი, რომლიდანაც ამოვიღებთ მხოლოდ დროს.</param>
        /// <returns>აბრუნებს შეცვლილ თარიღს.</returns>
        public static DateTime CombineEarliestDateTime(this DateTime time)
        {
            return CombineTime(EarliestDate, time);
        }
        /// <summary>
        /// უცვლის დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="date">თარიღი, რომელზეც გვინდა დროის შეცვლა.</param>
        /// <param name="time">თარიღი, რომლიდანაც ამოვიღებთ მხოლოდ დროს.</param>
        /// <returns>აბრუნებს შეცვლილ თარიღს.</returns>
        public static DateTime CombineTime(this DateTime date, DateTime time)
        {
            return CombineTime(date, time.Hour, time.Minute, time.Second);
        }
        /// <summary>
        /// უცვლის დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="date">თარიღი, რომელზეც გვინდა დროის შეცვლა.</param>
        /// <param name="hours">საათი.</param>
        /// <param name="minutes">წუთი.</param>
        /// <param name="seconds">წამი.</param>
        /// <returns>აბრუნებს შეცვლილ თარიღს.</returns>
        public static DateTime CombineTime(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.Date.Add(new TimeSpan(hours, minutes, seconds));
        }


        public static DateTime GetStartOfSecond(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }
        public static DateTime GetEndOfSecond(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 999);
        }
        public static DateTime GetStartOfMinute(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);
        }
        public static DateTime GetEndOfMinute(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 59, 999);
        }
        public static DateTime GetStartOfHour(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);
        }
        public static DateTime GetEndOfHour(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 59, 59, 999);
        }
        public static DateTime GetStartOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }
        public static DateTime GetStartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59, 999);
        }
        public static DateTime GetStartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0, 0);
        }
        public static DateTime GetEndOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31, 23, 59, 59, 999);
        }
        

        public static DateTime? GetStartOfSecond(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfSecond(date.Value) : null;
        }
        public static DateTime? GetEndOfSecond(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfSecond(date.Value) : null;
        }
        public static DateTime? GetStartOfMinute(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfMinute(date.Value) : null;
        }
        public static DateTime? GetEndOfMinute(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfMinute(date.Value) : null;
        }
        public static DateTime? GetStartOfHour(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfHour(date.Value) : null;
        }
        public static DateTime? GetEndOfHour(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfHour(date.Value) : null;
        }
        public static DateTime? GetStartOfDay(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfDay(date.Value) : null;
        }
        public static DateTime? GetEndOfDay(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfDay(date.Value) : null;
        }
        public static DateTime? GetStartOfMonth(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfMonth(date.Value) : null;
        }
        public static DateTime? GetEndOfMonth(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfMonth(date.Value) : null;
        }
        public static DateTime? GetStartOfYear(this DateTime? date)
        {
            return date != null ? (DateTime?)GetStartOfYear(date.Value) : null;
        }
        public static DateTime? GetEndOfYear(this DateTime? date)
        {
            return date != null ? (DateTime?)GetEndOfYear(date.Value) : null;
        }





        /// <summary>
        /// ამრგვალებს ფორმატის მოჭრით (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომლის დამრგვალებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა ფორმატში დამრგვალდეს.</param>
        /// <returns>აბრუნებს დამრგვალებულ თარიღს.</returns>
        public static DateTime RoundDown(this DateTime date, DateTimePrecision precission)
        {
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return GetStartOfSecond(date);

                case DateTimePrecision.Minutes:
                    return GetStartOfMinute(date);

                case DateTimePrecision.Hours:
                    return GetStartOfHour(date);

                case DateTimePrecision.Days:
                    return GetStartOfDay(date);
            }

            return GetStartOfSecond(date);
        }
        /// <summary>
        /// ამრგვალებს ფორმატის მოჭრით (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომლის დამრგვალებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა ფორმატში დამრგვალდეს.</param>
        /// <returns>აბრუნებს დამრგვალებულ თარიღს.</returns>
        public static DateTime? RoundDown(this DateTime? date, DateTimePrecision precission)
        {
            if (date == null) return null;
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return GetStartOfSecond(date.Value);

                case DateTimePrecision.Minutes:
                    return GetStartOfMinute(date.Value);

                case DateTimePrecision.Hours:
                    return GetStartOfHour(date.Value);

                case DateTimePrecision.Days:
                    return GetStartOfDay(date.Value);
            }

            return GetStartOfSecond(date.Value);
        }

        /// <summary>
        /// ამრგვალებს ფორმატის  დამატებით (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომლის დამრგვალებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა ფორმატში დამრგვალდეს.</param>
        /// <returns>აბრუნებს დამრგვალებულ თარიღს.</returns>
        public static DateTime RoundUp(this DateTime date, DateTimePrecision precission)
        {
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return GetEndOfSecond(date);

                case DateTimePrecision.Minutes:
                    return GetEndOfMinute(date);

                case DateTimePrecision.Hours:
                    return GetEndOfHour(date);

                case DateTimePrecision.Days:
                    return GetEndOfDay(date);
            }

            return GetEndOfSecond(date);
        }
        /// <summary>
        /// ამრგვალებს ფორმატის  დამატებით (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომლის დამრგვალებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა ფორმატში დამრგვალდეს.</param>
        /// <returns>აბრუნებს დამრგვალებულ თარიღს.</returns>
        public static DateTime? RoundUp(this DateTime? date, DateTimePrecision precission)
        {
            if (date == null) return null;
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return GetEndOfSecond(date.Value);

                case DateTimePrecision.Minutes:
                    return GetEndOfMinute(date.Value);

                case DateTimePrecision.Hours:
                    return GetEndOfHour(date.Value);

                case DateTimePrecision.Days:
                    return GetEndOfDay(date.Value);
            }

            return GetEndOfSecond(date.Value);
        }




        /// <summary>
        /// იგივეა რაც return Add(date, 1d, roundingFormat);
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precission"></param>
        /// <returns></returns>
        public static DateTime AddOneUnit(this DateTime date, DateTimePrecision precission)
        {
            return Add(date, 1d, precission);
        }
        /// <summary>
        /// იგივეა რაც return Add(date, 1d, roundingFormat);
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precission"></param>
        /// <returns></returns>
        public static DateTime? AddOneUnit(this DateTime? date, DateTimePrecision precission)
        {
            return Add(date, 1d, precission);
        }


        /// <summary>
        /// იგივეა რაც return Add(date, -1d, roundingFormat);
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precission"></param>
        /// <returns></returns>
        public static DateTime RemoveOneUnit(this DateTime date, DateTimePrecision precission)
        {
            return Add(date, -1d, precission);
        }
        /// <summary>
        /// იგივეა რაც return Add(date, -1d, roundingFormat);
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precission"></param>
        /// <returns></returns>
        public static DateTime? RemoveOneUnit(this DateTime? date, DateTimePrecision precission)
        {
            return Add(date, -1d, precission);
        }


        /// <summary>
        /// ამატებს ფორმატის მიხედვით დროს (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომელზეც დამატება გვინდა.</param>
        /// <param name="value">მნიშვნელობა რომლის დამატებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა მნიშვნელობა დაამატოს. წამი, წუთი, საათი...</param>
        /// <returns>აბრუნებს დამატებულ თარიღს.</returns>
        public static DateTime Add(this DateTime date, double value, DateTimePrecision precission)
        {
            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return date.AddSeconds(value);

                case DateTimePrecision.Minutes:
                    return date.AddMinutes(value);

                case DateTimePrecision.Hours:
                    return date.AddHours(value);

                case DateTimePrecision.Days:
                    return date.AddDays(value);
            }

            return date.AddSeconds(value);
        }
        /// <summary>
        /// ამატებს ფორმატის მიხედვით დროს (გამოიყენება ფილტრებისთვის).
        /// </summary>
        /// <param name="date">თარიღი რომელზეც დამატება გვინდა.</param>
        /// <param name="value">მნიშვნელობა რომლის დამატებაც გვინდა.</param>
        /// <param name="precission">ფორმატი რუ რა მნიშვნელობა დაამატოს. წამი, წუთი, საათი...</param>
        /// <returns>აბრუნებს დამატებულ თარიღს.</returns>
        public static DateTime? Add(this DateTime? date, double value, DateTimePrecision precission)
        {
            if (date == null) return null;

            switch (precission)
            {
                case DateTimePrecision.Seconds:
                    return date.Value.AddSeconds(value);

                case DateTimePrecision.Minutes:
                    return date.Value.AddMinutes(value);

                case DateTimePrecision.Hours:
                    return date.Value.AddHours(value);

                case DateTimePrecision.Days:
                    return date.Value.AddDays(value);
            }

            return date.Value.AddSeconds(value);
        }


        /// <summary>
        /// გადმოცემულ თარიღს ამატებს მითითებულ დროს.
        /// </summary>
        /// <param name="date">თარიღი.</param>
        /// <param name="hours">საათი.</param>
        /// <param name="minutes">წუთი.</param>
        /// <param name="seconds">წამი.</param>
        /// <returns>აბრუნებს გადმოცემულ თარიღზე უკვე დამატებულ დროს.</returns>
        public static DateTime Add(this DateTime date, int hours, int minutes, int seconds)
        {
            return date.Add(new TimeSpan(hours, minutes, seconds));
        }
        /// <summary>
        /// უმატებს დროს (საათი, წუთი, წამი).
        /// </summary>
        /// <param name="date">თარიღი, რომელზეც გვინდა დავამატოთ.</param>
        /// <param name="time">თარიღი, რომლიდანაც ამოვიღებთ მხოლოდ დროს და დავამატებთ.</param>
        /// <returns>აბრუნებს დამატებულ თარიღს.</returns>
        public static DateTime AddTime(this DateTime date, DateTime time)
        {
            return Add(date, time.Hour, time.Minute, time.Second);
        }


        public static bool IsBetween(this DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }


        /// <summary>
        /// აბრუნებს თარიღებს შორის თვეების რაოდენობას.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int SubtractMonth(this DateTime date1, DateTime date2)
        {
            return 12 * (date1.Year - date2.Year) + date1.Month - date2.Month;
        }
        /// <summary>
        /// აბრუნებს თარიღებს შორის თვეების რაოდენობას (დამრგვალებულს: თუ დღეების რაოდენობა 28 ან მეტია მაშინ 1 თვეს კიდე ამატებს).
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int SubtractMonthRound(this DateTime date1, DateTime date2)
        {
            return 12 * (date1.Year - date2.Year) + date1.Month - date2.Month + (date1.Day - date2.Day >= 27 ? 1 : (date2.Day - date1.Day >= 27 ? -1 : 0));
        }

        /// <summary>
        /// აბრუნებს ასაკს.
        /// </summary>
        /// <param name="birthDate">დაბადების თარიღი.</param>
        /// <param name="now">მიმდინარე თარიღი.</param>
        /// <returns>ასაკი.</returns>
        public static int GetAge(this DateTime birthDate, DateTime? now = null)
        {
            if (now == null)
                now = DateTime.Now;

            var age = now.Value.Year - birthDate.Year;
            if (birthDate <= now)
            {
                if (now.Value.Month < birthDate.Month || (now.Value.Month == birthDate.Month && now.Value.Day < birthDate.Day))
                    age--;
            }
            else
            {
                if (now.Value.Month > birthDate.Month || (now.Value.Month == birthDate.Month && now.Value.Day > birthDate.Day))
                    age++;
            }
            return age;
        }


        #region Bussiness
        /// <summary>
        /// ამოწმებს მოცემული თარიღი არის თუ არა შაბათი/კვირა.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsWeekEnd(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Saturday:
                    return true;

                default:
                    return false;
            }
        }

        ///// <summary>
        ///// ამატებს სამუშაო დღეებს.
        ///// </summary>
        ///// <param name="date">ათვლის თარიღი.</param>
        ///// <param name="businessDays">სამუშაო დღეების რაოდენობა.</param>
        ///// <returns>აბრუნებს სამუშაო დღის თარიღს.</returns>
        //public static DateTime AddBusinessDays(this DateTime date, int businessDays)
        //{
        //    var direction = businessDays < 0 ? -1 : 1;

        //    //თუ გადმოეცა 
        //    while (businessDays == 0 && IsWeekEnd(date))
        //        date = date.AddDays(1);

        //    while (businessDays != 0)
        //    {
        //        date = date.AddDays(direction);

        //        if (!IsWeekEnd(date))
        //            businessDays -= direction;
        //    }

        //    return date;
        //}

        //public static DateTime AddBusinessDays2(this DateTime date, int businessDays)
        //{
        //    var adjustedDate = date;

        //    /* First, we want to normalize the days to the upcoming Monday
        //     * Get the number of days to Monday
        //     */
        //    var daysUntilMonday = DaysUntilMonday(date);
        //    var daysToAdd = businessDays;

        //    // Too many days until Monday
        //    if (daysUntilMonday > daysToAdd)
        //    {
        //        adjustedDate = adjustedDate.AddDays(daysToAdd);

        //        // Check to see if we're on the weekend
        //        var endDateDaysUntilMonday = DaysUntilMonday(adjustedDate);

        //        // Landed on a weekend
        //        if (endDateDaysUntilMonday <= 2)
        //        {
        //            // Days from Friday - we need to add these back on to the end
        //            daysToAdd = 3 - endDateDaysUntilMonday;

        //            // Normalize to Sunday, then add the days we have not yet added
        //            adjustedDate = adjustedDate.AddDays(endDateDaysUntilMonday - 1 + daysToAdd);
        //        }
        //    }
        //    else
        //    {
        //        // Normalize to Monday
        //        adjustedDate = adjustedDate.AddDays(daysUntilMonday);

        //        daysToAdd -= Math.Max(daysUntilMonday - 2, 0);

        //        // Calculate the number of weekends added
        //        var weekendsAdded = (int)(daysToAdd / 5.0);
        //        daysToAdd += weekendsAdded * 2;

        //        // Add a day for each weekend day
        //        adjustedDate = adjustedDate.AddDays(daysToAdd);
        //    }

        //    return adjustedDate;
        //}
        //private static int DaysUntilMonday(DateTime date)
        //{
        //    var daysUntilMonday = DayOfWeek.Monday - date.DayOfWeek;

        //    if (daysUntilMonday < 0)
        //    {
        //        daysUntilMonday += 7;
        //    }

        //    return daysUntilMonday;
        //}

        /// <summary>
        /// ამატებს სამუშაო დღეებს.
        /// </summary>
        /// <param name="date">ათვლის თარიღი.</param>
        /// <param name="businessDays">სამუშაო დღეების რაოდენობა.</param>
        /// <returns>აბრუნებს სამუშაო დღის თარიღს.</returns>
        public static DateTime AddBusinessDays(this DateTime date, int businessDays)
        {
            var dayOfWeek = businessDays < 0 ? ((int)date.DayOfWeek - 12) % 7 : ((int)date.DayOfWeek + 6) % 7;

            switch (dayOfWeek)
            {
                case 6:
                    businessDays--;
                    break;
                case -6:
                    businessDays++;
                    break;
            }

            return date.AddDays(businessDays + (businessDays + dayOfWeek) / 5 * 2);
        }

        /// <summary>
        /// ამატებს დღეებს და თუ მოუწია შაბათი ან კვირა მაშინ ორშაბათამდე გადის.
        /// </summary>
        /// <param name="date">ათვლის თარიღი.</param>
        /// <param name="days">დღეების რაოდენობა.</param>
        /// <returns>აბრუნებს დამატებულ დღეებს ბოლო შაბათ-კვირის გადახტომით.</returns>
        public static DateTime AddDaysUntilMonday(this DateTime date, int days)
        {
            date = date.AddDays(days);

            if (IsWeekEnd(date))
            {
                var daysUntilMonday = DayOfWeek.Monday - date.DayOfWeek;
                if (daysUntilMonday < 0)
                    daysUntilMonday += 7;

                return date.AddDays(daysUntilMonday);
            }

            return date;
        }

        /// <summary>
        /// აბრუნებს სამუშაო დღეების რაოდენობას შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს სამუშაო დღეების რაოდენობას.</returns>
        public static int GetBusinessDays(this DateTime from, DateTime to)
        {
            var businessDays = 0;
            while (from < to)
            {
                from = from.AddDays(1);
                if (!IsWeekEnd(from))
                    businessDays += 1;
            }
            return businessDays;
        }
        /// <summary>
        /// აბრუნებს დასვენების დღეების რაოდენობას შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს დასვენების დღეების რაოდენობას.</returns>
        public static int GetWeekEndDays(this DateTime from, DateTime to)
        {
            var weekEndDays = 0;
            while (from < to)
            {
                from = from.AddDays(1);
                if (IsWeekEnd(from))
                    weekEndDays += 1;
            }
            return weekEndDays;
        }
        /// <summary>
        /// აბრუნებს სამუშაო დღეების მასივს მოცემულ შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს სამუშაო დღეების მასივს.</returns>
        public static DateTime[] GetBusinessDates(this DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var businessDates = new List<DateTime>();
            while (from < to)
            {
                from = from.AddDays(1d);
                if (!IsWeekEnd(from))
                    businessDates.Add(from);
            }

            return businessDates.ToArray();
        }
        /// <summary>
        /// აბრუნებს დასვენების დღეების მასივს მოცემულ შუალედში.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>აბრუნებს დასვენების დღეების მასივს.</returns>
        public static DateTime[] GetWeekEndDates(this DateTime from, DateTime to)
        {
            from = from.Date;
            to = to.Date;
            var weekEndDates = new List<DateTime>();
            while (from < to)
            {
                from = from.AddDays(1d);
                if (IsWeekEnd(from))
                    weekEndDates.Add(from);
            }

            return weekEndDates.ToArray();
        }
        #endregion
    }

 
}

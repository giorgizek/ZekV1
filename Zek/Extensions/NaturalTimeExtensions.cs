using System;

namespace Zek.Extensions
{
    public static class NaturalTimeExtensions
    {
        /// <summary>
        /// Renders the given date as a natural language string, appending the
        /// "ago" suffix like "10 minutes ago".
        /// </summary>
        /// <param name="date" this="true">The date to convert.</param>
        public static string ToNaturalRelativeTime(this DateTime date)
        {
            return ToNaturalString(date, true);
        }

        /// <summary>
        /// Renders the given date as a natural language string, like "10 minutes".
        /// </summary>
        /// <param name="date" this="true">The date to convert.</param>
        public static string ToNaturalTime(this DateTime date)
        {
            return ToNaturalString(date, false);
        }

        /// <summary>
        /// Renders the given date as a natural language string, appending the
        /// "ago" suffix like "10 minutes ago".
        /// </summary>
        /// <param name="date" this="true">The date to convert.</param>
        public static string ToNaturalRelativeTime(this DateTimeOffset date)
        {
            return ToNaturalString(date.UtcDateTime, true);
        }

        /// <summary>
        /// Renders the given date as a natural language string, like "10 minutes".
        /// </summary>
        /// <param name="date" this="true">The date to convert.</param>
        public static string ToNaturalTime(this DateTimeOffset date)
        {
            return ToNaturalString(date.UtcDateTime, false);
        }

        private static string ToNaturalString(this DateTime date, bool prependAgo)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                throw new NotSupportedException("Date must be in UTC or local time. Unsupported 'Unspecified' date kind.");

            if (date.Kind != DateTimeKind.Utc)
                date = date.ToUniversalTime();

            var totalSeconds = (DateTime.UtcNow - date).TotalSeconds;

            var years = Convert.ToInt32(Math.Floor(totalSeconds / (365.242199 * 24 * 60 * 60)));
            if (years >= 1)
            {
                return string.Format(years == 1 ?
                    (prependAgo ? "{0} year ago" : "{0} year") :
                    (prependAgo ? "{0} years ago" : "{0} years"),
                    years);
            }
            var months = Convert.ToInt32(Math.Floor(totalSeconds / (30.4368499 * 24 * 60 * 60)));
            if (months >= 1)
            {
                return string.Format(months == 1 ?
                    (prependAgo ? "{0} month ago" : "{0} month") :
                    (prependAgo ? "{0} months ago" : "{0} months"),
                    months);
            }

            var weeks = Convert.ToInt32(Math.Floor(totalSeconds / (7 * 24 * 60 * 60)));
            if (weeks >= 1)
            {
                return string.Format(weeks == 1 ?
                    (prependAgo ? "{0} week ago" : "{0} week") :
                    (prependAgo ? "{0} weeks ago" : "{0} weeks"),
                    weeks);
            }

            var days = Convert.ToInt32(Math.Floor(totalSeconds / (24 * 60 * 60)));
            if (days >= 1)
            {
                return string.Format(days == 1 ?
                    (prependAgo ? "{0} day ago" : "{0} day") :
                    (prependAgo ? "{0} days ago" : "{0} days"),
                    days);
            }

            var hours = Convert.ToInt32(Math.Floor(totalSeconds / (60 * 60)));
            if (hours >= 1)
            {
                return string.Format(hours == 1 ?
                    (prependAgo ? "{0} hour ago" : "{0} hour") :
                    (prependAgo ? "{0} hours ago" : "{0} hours"),
                    hours);
            }

            var minutes = Convert.ToInt32(Math.Floor(totalSeconds / 60));
            if (minutes < 0) minutes = 0;

            return string.Format(minutes == 1 ?
                (prependAgo ? "{0} min ago" : "{0} min") :
                (prependAgo ? "{0} mins ago" : "{0} mins"),
                minutes);
        }

    }
}
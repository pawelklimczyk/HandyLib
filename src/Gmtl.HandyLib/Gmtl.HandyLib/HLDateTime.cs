// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLDateTime.cs" project="Gmtl.HandyLib" date="2015-10-16 18:23">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy methods related to System.DateTime
    /// </summary>
    public static class HLDateTime
    {
        private static DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Return local Linux timestamp 
        /// </summary>
        /// <remarks>
        /// <code>
        /// int unixTimestamp = HLDateTime.NowUnixTimestamp;
        /// </code>
        /// </remarks>
        public static long NowUnixTimestamp
        {
            get
            {
                return ToUnixTimestamp(DateTime.Now);
            }
        }

        /// <summary>
        /// Convert unix timestamp to System.DateTime
        /// </summary>
        /// <param name="timestamp">Unix timestamp to be converted</param>
        /// <returns>DateTime representation of unix timestamp</returns>
        public static DateTime FromUnixTimestamp(this long timestamp)
        {
            if (timestamp > 9999999999)
            {
                return unixStart.AddMilliseconds(timestamp);
            }
            else
            {
                return unixStart.AddSeconds(timestamp);
            }
        }

        /// <summary>
        /// Convert System.DateTime to unix timestamp
        /// </summary>
        /// <param name="dateTime">DateTime struct to be converted</param>
        /// <returns>unix timestamp representation for the provided DateTime</returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime - unixStart).TotalSeconds;
        }

        /// <summary>
        /// Return -date- 23:59:59.999
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddMilliseconds(-1);
        }
        /// <summary>
        /// Return -date- 00:00:00.000
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToStartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        //TODO create setup method to support different languages
        private static string _lessThanHourAgo = "nie całą godzinę temu";
        private static string _someMinutesAgo = " minut temu";
        private static string _someHoursAgo = " godzin temu";
        private static string _someDaysAgo = " dni temu";
        private static string _oneWeekAgo = "tydzień temu";
        private static string _twoWeeksAgo = "2 tygodnie temu";
        private static string _overMonthAgo = "ponad miesiąc temu";
        private static string _yesterday = "wczoraj";
        private static string _now = "Teraz";

        /// <summary>
        /// Return user-friendly date description
        /// </summary>
        public static string TimeAgo(this DateTime time)
        {
            return TimeAgo(time, DateTime.Now);
        }

        /// <summary>
        /// Return user-friendly date description using provided reference time.
        /// </summary>
        public static string TimeAgo(this DateTime time, DateTime referenceTime)
        {
            var difference = referenceTime - time;
            var wholeMinutes = (int)difference.TotalMinutes;
            var wholeHours = (int)difference.TotalHours;

            if (difference.TotalSeconds < 0)
            {
                return _now;
            }

            if (difference.TotalMinutes < 1)
            {
                return _now;
            }

            if (difference.TotalHours < 1)
            {
                return wholeMinutes + _someMinutesAgo;
            }

            if (time.Date == referenceTime.Date)
            {
                return wholeHours + _someHoursAgo;
            }

            if (time.Date == referenceTime.Date.AddDays(-1))
            {
                return _yesterday;
            }

            if (difference.TotalDays >= 30)
            {
                return _overMonthAgo;
            }

            if (difference.TotalDays >= 14)
            {
                return _twoWeeksAgo;
            }

            if (difference.TotalDays >= 7)
            {
                return _oneWeekAgo;
            }

            if (difference.TotalDays >= 2)
            {
                return ((int)difference.TotalDays) + _someDaysAgo;
            }

            return _yesterday;
        }
    }
}
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
        private static string _someHoursAgo = " godzin temu";
        private static string _someDaysAgo = " dni temu";
        private static string _yesterday = "wczoraj";

        /// <summary>
        /// Return user-friendly date description
        /// </summary>
        public static string TimeAgo(this DateTime time)
        {
            var difference = DateTime.Today - time;

            if (difference.TotalDays < 0)
            {
                if (difference.TotalHours < 0)
                {
                    return _lessThanHourAgo;
                }

                return ((int)difference.TotalHours) + _someHoursAgo;
            }

            if (difference.TotalDays >= 2)
            {
                return ((int)difference.TotalDays) + _someDaysAgo;
            }

            return _yesterday;
        }
    }
}
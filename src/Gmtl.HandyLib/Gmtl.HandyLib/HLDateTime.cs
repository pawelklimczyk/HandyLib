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
        /// Return Linux timestamp for provided date
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
        public static DateTime FromUnixTimestamp(long timestamp)
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
        /// <returns>unix timestamp representation for DateTime</returns>
        public static long ToUnixTimestamp(DateTime dateTime)
        {
            return (long)(dateTime - unixStart).TotalSeconds;
        }
    }
}
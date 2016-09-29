// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLDateTimeTests.cs" project="Gmtl.HandyLib.Tests" date="2016-09-29 16:29">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLDateTimeTests
    {
        [TestCase(1454414400, "2016-02-02 12:00:00")]
        [TestCase(946684800, "2000-01-01 00:00:00")]
        public void HLDateTime_UnixTimestampDateShouldBeConvertedToDateTime(long timestamp, string expectedDate)
        {
            DateTime dateExpected = DateTime.ParseExact(expectedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            var dateConverted = HLDateTime.FromUnixTimestamp(timestamp);

            Assert.AreEqual(dateConverted, dateExpected);
        }

        [TestCase("2016-02-02 12:00:01", 1454414401)]
        [TestCase("2000-01-01 00:00:00", 946684800)]
        public void HLDateTime_DateShouldBeConvertedToUnixTimestamp(string date, long expectedTimestamp)
        {
            DateTime dateExpected = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            var actualTimestamp = HLDateTime.ToUnixTimestamp(dateExpected);

            Assert.AreEqual(actualTimestamp, expectedTimestamp);
        }

        public void HLDateTime_DateShouldBeConvertedToUnixTimestampAndBack()
        {
            DateTime startDate = DateTime.Now;

            var actualTimestamp = HLDateTime.ToUnixTimestamp(startDate);
            var endDate = HLDateTime.FromUnixTimestamp(actualTimestamp);

            Assert.AreEqual(startDate, endDate);
        }
    }
}
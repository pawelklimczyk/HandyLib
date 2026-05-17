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
        [TestCase(1565259922243, "2019-08-08 10:25:22.243")]
        public void HLDateTime_UnixTimestampDateShouldBeConvertedToDateTime(long timestamp, string expectedDate)
        {
            DateTime dateExpected = DateTime.ParseExact(expectedDate, "yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.CurrentCulture);

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

        [TestCase("2016-02-02 12:00:01", "2016-02-02 23:59:59.999")]
        [TestCase("2000-01-01 19:00:30", "2000-01-01 23:59:59.999")]
        public void HLDateTime_DateShouldBeEndOfDay(string date, string expectedDate)
        {
            DateTime dateExpected = DateTime.ParseExact(expectedDate, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.CurrentCulture);

            var actualTimestamp = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture).ToEndOfDay();

            Assert.AreEqual(dateExpected, actualTimestamp);
        }

        [TestCase("2016-02-02 12:00:01", "2016-02-02 00:00:00")]
        [TestCase("2000-01-01 19:00:30", "2000-01-01 00:00:00")]
        public void HLDateTime_DateShouldBeStartOfDay(string date, string expectedDate)
        {
            DateTime dateExpected = DateTime.ParseExact(expectedDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            var actualTimestamp = DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture).ToStartOfDay();

            Assert.AreEqual(dateExpected, actualTimestamp);
        }

        [Test]
        public void HLDateTime_DateShouldBeConvertedToUnixTimestampAndBack()
        {
            DateTime startDate = DateTime.ParseExact("2024-05-17 12:34:56", "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            var actualTimestamp = HLDateTime.ToUnixTimestamp(startDate);
            var endDate = HLDateTime.FromUnixTimestamp(actualTimestamp);

            Assert.AreEqual(startDate, endDate);
        }

        [TestCase("2026-05-17 22:00:00", 120, "Teraz")]
        [TestCase("2026-05-17 22:00:00", 0, "Teraz")]
        [TestCase("2026-05-17 22:00:00", -5, "5 minut temu")]
        [TestCase("2026-05-17 22:00:00", -30, "30 minut temu")]
        [TestCase("2026-05-17 22:00:00", -120, "2 godzin temu")]
        [TestCase("2026-05-17 22:00:00", -720, "12 godzin temu")]
        [TestCase("2026-05-17 22:00:00", -1080, "18 godzin temu")]
        [TestCase("2026-05-17 08:00:00", -720, "wczoraj")]
        [TestCase("2026-05-17 12:00:00", -4320, "3 dni temu")]
        [TestCase("2026-05-17 12:00:00", -7200, "5 dni temu")]
        [TestCase("2026-05-17 12:00:00", -10080, "tydzień temu")]
        [TestCase("2026-05-17 12:00:00", -20160, "2 tygodnie temu")]
        [TestCase("2026-05-17 12:00:00", -44640, "ponad miesiąc temu")]
        public void HLDateTime_TimeAgoShouldReturnExpectedText(string startDate, int minutesOffset, string expectedText)
        {
            DateTime referenceTime = DateTime.ParseExact(startDate, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
            var actualText = referenceTime.AddMinutes(minutesOffset).TimeAgo(referenceTime);

            Assert.AreEqual(expectedText, actualText);
        }
    }
}
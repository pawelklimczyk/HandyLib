using System;
using System.Globalization;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLFormattingExtensionsTests
    {
        [TearDown]
        public void TearDown()
        {
            HLFormattingExtensions.SetCulture(CultureInfo.CreateSpecificCulture("pl-PL"));
        }

        [Test]
        public void ToDateString_WithDateTime_ShouldFormatAsIsoDate()
        {
            var date = new DateTime(2023, 7, 4);

            Assert.AreEqual("2023-07-04", date.ToDateString());
        }

        [Test]
        public void ToDateTimeString_WithDateTime_ShouldFormatWithHoursAndMinutes()
        {
            var date = new DateTime(2023, 7, 4, 13, 45, 0);

            Assert.AreEqual("2023-07-04 13:45", date.ToDateTimeString());
        }

        [Test]
        public void ToDateString_WithNullNullableDateTime_ShouldReturnDash()
        {
            DateTime? date = null;

            Assert.AreEqual("-", date.ToDateString());
        }

        [Test]
        public void ToDateString_WithValueNullableDateTime_ShouldFormatAsIsoDate()
        {
            DateTime? date = new DateTime(2023, 7, 4);

            Assert.AreEqual("2023-07-04", date.ToDateString());
        }

        [Test]
        public void ToDateTimeString_WithNullNullableDateTime_ShouldReturnDash()
        {
            DateTime? date = null;

            Assert.AreEqual("-", date.ToDateTimeString());
        }

        [Test]
        public void ToDateTimeString_WithValueNullableDateTime_ShouldFormatWithHoursAndMinutes()
        {
            DateTime? date = new DateTime(2023, 7, 4, 13, 45, 0);

            Assert.AreEqual("2023-07-04 13:45", date.ToDateTimeString());
        }

        [Test]
        public void ToCurrency_ShouldFormatUsingCurrentCultureSymbol()
        {
            decimal value = 1234.5m;

            string result = value.ToCurrency();

            StringAssert.Contains("1", result);
            StringAssert.Contains(value.CurrencySymbol(), result);
        }

        [Test]
        public void ToCurrencyValue_ShouldFormatWithTwoDecimalPlacesAndNoCurrencySymbol()
        {
            decimal value = 1234.5m;

            string result = value.ToCurrencyValue();

            StringAssert.DoesNotContain(value.CurrencySymbol(), result);
        }

        [Test]
        public void CurrencySymbol_ShouldReturnCurrentCultureCurrencySymbol()
        {
            HLFormattingExtensions.SetCulture(CultureInfo.CreateSpecificCulture("en-US"));

            Assert.AreEqual("$", 1m.CurrencySymbol());
        }

        [Test]
        public void SetCulture_ShouldChangeFormattingOfSubsequentCalls()
        {
            var date = new DateTime(2023, 7, 4);

            HLFormattingExtensions.SetCulture(CultureInfo.CreateSpecificCulture("en-US"));

            Assert.AreEqual("2023-07-04", date.ToDateString());
        }
    }
}

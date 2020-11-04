// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLFormattingHelpers.cs" project="Gmtl.HandyLib" date="2020-11-04 11:03">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace Gmtl.HandyLib
{
    public static class HLFormattingExtensions
    {
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("pl-PL");

        public static void SetCulture(CultureInfo newCulture)
        {
            culture = newCulture;
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd", culture);
        }

        public static string ToDateTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm", culture);
        }

        public static string ToDateString(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd", culture);
            }

            return "-";
        }

        public static string ToDateTimeString(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd HH:mm", culture);
            }

            return "-";
        }

        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C2", culture);
        }

        public static string ToCurrencyValue(this decimal value)
        {
            return value.ToString("N2", culture);
        }

        public static string CurrencySymbol(this decimal value)
        {
            return culture.NumberFormat.CurrencySymbol;
        }
    }
}
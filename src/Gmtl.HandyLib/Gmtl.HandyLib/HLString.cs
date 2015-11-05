// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLString.cs" project="Gmtl.HandyLib" date="2015-10-22 20:23:01">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib
{
    public static class HLString
    {
        /// <summary>
        /// Returns input if it's not null or whitespace, defaultValue otherwise
        /// </summary>
        public static string ValueOrDefault(string input, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        /// <summary>
        /// Returns input if it's not null or whitespace, String.Empty otherwise
        /// </summary>
        public static string ValueOrEmpty(string input)
        {
            return ValueOrDefault(input, String.Empty);
        }
    }
}
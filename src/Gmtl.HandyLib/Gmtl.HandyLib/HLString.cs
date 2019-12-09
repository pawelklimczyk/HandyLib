// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial" file="HLString.cs" project="Gmtl.HandyLib" date="2015-10-22 20:23:01">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Handy methods related to System.String
    /// </summary>
    public static class HLString
    {
        /// <summary>
        /// Returns input if it's not null or whitespace, defaultValue otherwise
        /// </summary>
        /// <remarks>
        /// <code>
        /// HLString.ValueOrDefault("test", "replaced"); //'test' returned
        /// HLString.ValueOrDefault("", "replaced"); //'replaced' returned
        /// HLString.ValueOrDefault(null, "replaced"); //'replaced' returned
        /// </code>
        /// </remarks>
        public static string ValueOrDefault(string input, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        /// <summary>
        /// Returns input if it's not null or whitespace, String.Empty otherwise
        /// </summary>
        /// <remarks>
        /// <code>
        /// HLString.ValueOrEmpty("test"); //'test' returned
        /// HLString.ValueOrEmpty(null); //'String.Empty' returned
        /// </code>
        /// </remarks>
        public static string ValueOrEmpty(string input)
        {
            return ValueOrDefault(input, String.Empty);
        }

        /// <summary>
        /// Returns truncated string
        /// </summary>
        public static string MaxOf(string input, int lettersCount, string suffix = "")
        {
            string sanitized = ValueOrDefault(input, String.Empty);

            if (sanitized.Length <= lettersCount)
                return sanitized;

            return sanitized.Substring(0, lettersCount) + suffix;
        }
    }
}